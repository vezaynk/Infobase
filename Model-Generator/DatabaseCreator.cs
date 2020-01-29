using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Collections.ObjectModel;
using Models.Metadata;
using CsvHelper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Design;

namespace Model_Generator
{
    public class DatabaseCreator
    {
        public DatabaseCreator(string connectionString, string datasetName, Assembly asm = null)
        {
            this.ConnectionString = connectionString;
            this.DatasetName = datasetName;
            var dbContextAssembly = asm ?? DbContextBuilder.BuildDbContextAssembly(DatasetName);
            this.DbContext = DbContextBuilder.GetDBContext(dbContextAssembly, $"Models.Contexts.{DatasetName}.Context", ob => ob.UseNpgsql(ConnectionString));
        }
        public string DatasetName { get; set; }
        public DbContext DbContext { get; set; }
        public string ConnectionString { get; set; }
        public DbContext ReloadDbContext()
        {
            var migrationsAssembly = DbContextBuilder.BuildDbContextAssembly(DatasetName);
            this.DbContext = DbContextBuilder.GetDBContext(migrationsAssembly, $"Models.Contexts.{DatasetName}.Context", ob => ob.UseNpgsql(ConnectionString, o => o.MigrationsAssembly(migrationsAssembly.GetName().ToString())));
            return DbContext;
        }
        public bool CleanDatabase()
        {
            try
            {
                return DbContext.Database.EnsureDeleted();
            }
            catch (Npgsql.PostgresException)
            {
                Console.Write("Failed to clean database (check permissions or so manually)...");
                return false;
            }

        }
        public void CreateDatabase()
        {
            DbContext.Database.EnsureCreated();
        }
        // We are working with an Enumerable. I'm not sure why, but trying to narrow it down to a Queryable breaks the program
        public IEnumerable<dynamic> GetDbSet(Type setType)
        {
            var genericDbSetMethod = DbContext.GetType().GetMethod("Set").MakeGenericMethod(new[] { setType });

            return Enumerable.Cast<dynamic>((IEnumerable)genericDbSetMethod.Invoke(DbContext, new object[] { }));
        }
        public int LoadMasterCSV(StreamReader sr)
        {
            var dbContext = DbContext;
            // Load Master specifically
            Type masterType = dbContext.GetType().Assembly.GetTypes().First(t => t.Namespace == $"Models.Contexts.{this.DatasetName}" && t.Name == "Master");

            // Get master set for insertion
            var masterDbSet = GetDbSet(masterType);
            var addToMasterDbSet = new Action<object>(instance => masterDbSet.GetType().GetMethod("Add").Invoke(masterDbSet, new object[] { instance }));

            using (var csv = new CsvReader(sr, new CsvHelper.Configuration.Configuration
            {
                Delimiter = ",",
                Encoding = Encoding.UTF8
            }))
            {

                var records = csv.GetRecords<dynamic>();
                var masterInstances = records.Select((record, index) =>
                {
                    // Each record is a dictionary (header name => cell value)
                    var dict = (IDictionary<string, object>)record;
                    // Create an instance of a master record for each row
                    dynamic masterInstance = Activator.CreateInstance(masterType);

                    // Get all CSV properties
                    var csvProperties = masterType.GetProperties()
                    .Where(p => p.GetCustomAttribute(typeof(CSVColumnAttribute)) != null);

                    // Apply them all
                    foreach (var property in csvProperties)
                    {
                        string column = property.GetCustomAttribute<CSVColumnAttribute>().CSVColumnName;
                        dict.TryGetValue(column, out var value);
                        
                        if (!dict.ContainsKey(column))
                            throw new Exception($"Column with name {column} not found in CSV");

                        property.SetValue(masterInstance, value ?? "");
                    }

                    // Set index to maintain consistent order with CSV
                    masterInstance.Index = index + 1;

                    return masterInstance;
                });
                foreach (var masterInstance in masterInstances)
                {
                    addToMasterDbSet(masterInstance);
                };

                dbContext.SaveChanges();
                return masterDbSet.Count();
            }
        }

        public ICollection<string> LoadEntitiesFromMaster(IDictionary<string, string> translations = null)
        {
            var missingTranslations = new HashSet<string>();

            var dbContext = DbContext;
            Type masterType = dbContext.GetType().Assembly.GetTypes().First(t => t.Namespace == $"Models.Contexts.{this.DatasetName}" && t.Name == "Master");

            var masterDbSet = GetDbSet(masterType);

            var types = dbContext.GetType().Assembly.GetTypes()
                // Load all models, excluding the non-filter ones (Only the Master is excluded).
                .Where(t => t.Namespace == $"Models.Contexts.{this.DatasetName}" && t.GetCustomAttribute<FilterAttribute>() != null)
                .OrderBy(t => t.GetCustomAttribute<FilterAttribute>().Level);


            // Skip the last type, as it does not have any children
            foreach (Type type in types.SkipLast(1))
            {
                Console.WriteLine("Processing Type: " + type.Name);

                int currentLevel = type.GetCustomAttribute<FilterAttribute>().Level;

                // The child of the current type is the type whose Filter level is one greater
                Type childType = types.FirstOrDefault(t => t.GetCustomAttribute<FilterAttribute>().Level == currentLevel + 1);

                // The columns on which we need to distinguish are all of the CSV columns used by the child and each other parents
                // For example the children of currentLevel = 0 will need the data from the top (+1), as well as their own (+2)  
                var boundMasterPropertiesChild = types.Take(currentLevel + 2).SelectMany(t => t.GetProperties())
                    .Where(p => p.GetCustomAttribute<BindToMasterAttribute>() != null);

                // Get all the actual column names
                var distinctMasterPropertyNamesChild = boundMasterPropertiesChild
                        .Select(p => p.GetCustomAttribute<BindToMasterAttribute>().MasterPropertyName);

                // Get the dbset for the current and its child
                var currentDbSet = GetDbSet(type);
                var childDbSet = GetDbSet(childType);

                // As the key to this working is iterating through each parent and finding their children, the first filter must be loaded earlier
                if (type == types.First())
                {
                    Console.Write("Loading top-level entities...");
                    var boundMasterProperties = types.Take(currentLevel + 1).SelectMany(t => t.GetProperties())
                        .Where(p => p.GetCustomAttribute<BindToMasterAttribute>() != null);

                    var distinctMasterPropertyNames = boundMasterProperties
                            .Select(p => p.GetCustomAttribute<BindToMasterAttribute>().MasterPropertyName);

                    var topLevelRows = masterDbSet
                                .OrderBy(entity => entity.Index)
                                .DistinctBy(e =>
                                {
                                    var currentEntityIndex = e.Index;

                                    var distinctProperties = ((object)e).GetType().GetProperties()
                                        .Where(p => distinctMasterPropertyNames
                                            .Contains(p.Name));

                                    return string.Join("", distinctProperties.Select(p => p.GetValue(e)));
                                }).Select(e =>
                                {
                                    dynamic instance = Activator.CreateInstance(type);
                                    var currentEntityIndex = e.Index;
                                    instance.Index = currentEntityIndex;

                                    foreach (var boundProperty in boundMasterProperties)
                                    {
                                        var source = masterType.GetProperty(boundProperty.GetCustomAttribute<BindToMasterAttribute>().MasterPropertyName);
                                        try
                                        {
                                            boundProperty.SetValue(instance, source.GetValue(e));
                                        }
                                        catch
                                        {

                                        }

                                    }

                                    return instance;
                                });
                    foreach (var topLevelRow in topLevelRows)
                    {
                        currentDbSet.GetType().GetMethod("Add").Invoke(currentDbSet, new object[] { topLevelRow });
                    }
                    dbContext.SaveChanges();
                    Console.WriteLine($"Loaded {currentDbSet.Count()} entities!");
                }


                Console.Write($"Looking for children...");
                IEnumerable<dynamic> childRows = masterDbSet.OrderBy(e => e.Index)
                                        .DistinctBy(e =>
                                        {

                                            var distinctProperties = ((object)e).GetType().GetProperties()
                                                .Where(p => distinctMasterPropertyNamesChild
                                                    .Contains(p.Name));


                                            return string.Join("", distinctProperties.Select(p => p.GetValue(e)));
                                        }).ToList();

                var totalChildrenCount = childRows.Count();
                var usedIndexes = new HashSet<int>();

                Console.Write($"\rLooking for {totalChildrenCount} children...");
                foreach (var parent in currentDbSet)
                {
                    var parentId = type.GetProperties().First(property => property.Name == type.Name + "Id").GetValue(parent);
                    var childIndexProperty = childType.GetProperties().First(prop => prop.Name == "Index");

                    bool included = Metadata.GetIncludedState(parent);

                    childRows = childRows.Where(e => !usedIndexes.Contains(e.Index));
                    var children = childRows.Where((child, i) =>
                    {
                        if (!included)
                        {
                            return false;
                        }
                        // Filtering is fast. No significant optimization is possible.
                        var nextParent = parent;
                        while (nextParent != null)
                        {
                            foreach (var parentProperty in boundMasterPropertiesChild)
                            {
                                var columnName = parentProperty.GetCustomAttribute<BindToMasterAttribute>().MasterPropertyName;
                                var childProperty = masterType.GetProperty(columnName);

                                var childValue = childProperty.GetValue(child);

                                try
                                {
                                    var parentValue = parentProperty.GetValue(nextParent);
                                    if (Convert.ToString(childValue) != Convert.ToString(parentValue))
                                    {
                                        return false;
                                    }
                                }
                                catch (TargetException)
                                {
                                }

                            }
                            var nextParentLevel = ((object)nextParent).GetType().GetCustomAttribute<FilterAttribute>().Level;
                            string name = types.Skip((int)nextParentLevel - 1).FirstOrDefault()?.Name;
                            nextParent = Metadata.GetParentOf(nextParent);
                        }
                        return true;
                    }).Select((e, i) =>
                    {
                        var instance = Activator.CreateInstance(childType);
                        var currentEntityIndex = (int)e.Index;
                        childIndexProperty.SetValue(instance, currentEntityIndex);
                        usedIndexes.Add(currentEntityIndex);
                        foreach (var boundProperty in boundMasterPropertiesChild)
                        {
                            var source = masterType.GetProperty(boundProperty.GetCustomAttribute<BindToMasterAttribute>().MasterPropertyName);
                            try
                            {
                                if (source.PropertyType != boundProperty.PropertyType)
                                {
                                    //Todo handle non-string types
                                    throw new ArrayTypeMismatchException($"Type mismatch between master property {source.Name} and {boundProperty.Name}");
                                }
                                boundProperty.SetValue(instance, source.GetValue(e));
                            }
                            catch (TargetException)
                            {

                            }

                        }
                        childType.GetProperty(type.Name + "Id").SetValue(instance, parentId);
                        childType.GetProperty($"{childType.Name}Id").SetValue(instance, usedIndexes.Count());
                        return instance;
                    });

                    foreach (var child in children)
                    {
                        Console.Write($"\rFound {usedIndexes.Count()} out of {totalChildrenCount} children...");

                        childDbSet.GetType().GetMethod("Add").Invoke(childDbSet, new object[] { child });
                    }
                }
                dbContext.SaveChanges();
                Console.WriteLine();
            }

            foreach (Type type in types.SkipLast(1).Reverse())
            {
                var dbset = GetDbSet(type);
                var defaultChildProperty = Metadata.FindPropertyOnType<DefaultChildAttribute>(type);
                var childType = defaultChildProperty.PropertyType;
                var childDefaultChildProperty = Metadata.FindPropertyOnType<DefaultChildAttribute>(childType);

                foreach (var entity in dbset)
                {
                    var children = Enumerable.Cast<dynamic>((IEnumerable)Metadata.FindPropertyOnType<ChildrenAttribute>(type).GetValue((object)entity));
                    var defaultChild = children.FirstOrDefault(c => (types.SkipLast(1).Last() == type || childDefaultChildProperty.GetValue(c) != null));
                    if (defaultChild != null)
                        defaultChildProperty.SetValue(entity, defaultChild);
                }

                dbContext.SaveChanges();
            }


            Console.WriteLine($"Finished importing dataset");

            if (translations != null)
            {
                Console.Write("Loading translations...");
                foreach (Type type in types)
                {
                    var dbset = GetDbSet(type);
                    var properties = type.GetProperties().Where(p => p.GetCustomAttribute<TextAttribute>()?.Culture == "fr-ca" && p.GetCustomAttribute<TranslatePropertyAttribute>()?.Property != null);

                    foreach (var entity in dbset)
                    {
                        foreach (var translatedProperty in properties)
                        {
                            var sourcePropertyName = translatedProperty.GetCustomAttribute<TranslatePropertyAttribute>().Property;
                            var sourceProperty = type.GetProperty(sourcePropertyName);
                            var sourceValue = (string)sourceProperty.GetValue((object)entity);
                            translations.TryGetValue(sourceValue ?? "", out var translatedValue);

                            if (translatedValue == null)
                            {
                                missingTranslations.Add(sourceValue);
                            }
                            else
                            {
                                translatedProperty.SetValue((object)entity, translatedValue);
                            }

                        }
                    }

                }
                dbContext.SaveChanges();
                Console.WriteLine("Done");
            }


            return missingTranslations;
        }


    }
}