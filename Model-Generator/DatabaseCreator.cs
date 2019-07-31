using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
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
        public DatabaseCreator(string connectionString, string migrationsDirectory, string modelsDirectory, string datasetName) : this(connectionString, DbContextBuilder.BuildAssembly(datasetName, modelsDirectory, null, migrationsDirectory), datasetName)
        {
            this.MigrationsDirectory = migrationsDirectory;
            this.LoadedFromSource = true;
            this.ReloadDbContextLambda = () => (new DatabaseCreator(connectionString, DbContextBuilder.BuildAssembly(datasetName, modelsDirectory, PendingMigrations, migrationsDirectory), datasetName)).DbContext;
        }
        public DatabaseCreator(string connectionString, string pathToAssembly, string datasetName) : this(connectionString, Assembly.LoadFrom(pathToAssembly), datasetName)
        {
            this.ReloadDbContextLambda = () => (new DatabaseCreator(connectionString, pathToAssembly, datasetName)).DbContext;
        }

        public DatabaseCreator(string connectionString, Assembly assembly, string datasetName)
        {
            this.DbContext = DbContextBuilder.GetDBContext(assembly, $"Models.Contexts.{datasetName}.Context", ob => ob.UseNpgsql(connectionString, o => o.MigrationsAssembly(assembly.GetName().ToString())));
            PendingMigrations = new Collection<ScaffoldedMigration>();
            this.DatasetName = datasetName;
            this.ReloadDbContextLambda = () => (new DatabaseCreator(connectionString, assembly, datasetName)).DbContext;

        }
        public string DatasetName { get; set; }
        public DbContext DbContext { get; set; }
        public bool LoadedFromSource { get; set; }
        public string MigrationsDirectory { get; set; }
        private Func<DbContext> ReloadDbContextLambda { get; set; }
        public Collection<ScaffoldedMigration> PendingMigrations { get; set; }
        public DbContext ReloadDbContext()
        {
            DbContext = ReloadDbContextLambda();
            return DbContext;
        }
        public bool CleanDatabase()
        {
            return DbContext.Database.EnsureDeleted();
        }
        public ScaffoldedMigration CreateMigration(string name = null)
        {
            var mg = new MigrationGenerator(DbContext, name ?? DatasetName);
            var migration = mg.CreateMigration();
            PendingMigrations.Add(migration);
            return migration;
        }
        public void ApplyMigrations()
        {
            DbContext.Database.Migrate();
        }
        // We are working with an Enumerable. I'm not sure why, but trying to narrow it down to a Queryable breaks the program
        public IEnumerable<object> GetDbSet(Type setType)
        {
            var genericDbSetMethod = DbContext.GetType().GetMethod("Set").MakeGenericMethod(new[] { setType });
            return Enumerable.Cast<object>((IEnumerable)genericDbSetMethod.Invoke(DbContext, new object[] { }));
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
                var masterInstances = records.AsParallel().Select((record, index) =>
                {
                    // Each record is a dictionary (header name => cell value)
                    var dict = (IDictionary<string, object>)record;
                    // Create an instance of a master record for each row
                    var masterInstance = Activator.CreateInstance(masterType);

                    // Get all CSV properties
                    var csvProperties = masterType.GetProperties()
                    .Where(p => p.GetCustomAttribute(typeof(CSVColumnAttribute)) != null);

                    // Apply them all
                    foreach (var property in csvProperties)
                    {
                        string column = property.GetCustomAttribute<CSVColumnAttribute>().CSVColumnName;
                        dict.TryGetValue(column, out var value);
                        if (value == null)
                            throw new Exception($"Column with name {column} not found in CSV");

                        property.SetValue(masterInstance, value);
                    }

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
        public void SaveMigrations()
        {
            foreach (var migration in PendingMigrations)
            {
                string migrationPath = Path.Join(MigrationsDirectory, migration.MigrationId + migration.FileExtension);
                Console.WriteLine($"Migration: {migrationPath}");
                File.WriteAllText(migrationPath,
                        migration.MigrationCode);

                string designerPath = Path.Join(MigrationsDirectory, migration.MigrationId + ".Designer" + migration.FileExtension);
                File.WriteAllText(designerPath,
                    migration.MetadataCode);

                string snapshotPath = Path.Join(MigrationsDirectory, migration.SnapshotName + migration.FileExtension);
                File.WriteAllText(snapshotPath,
                   migration.SnapshotCode);
            }

        }

        public void LoadEntitiesFromMaster()
        {
            var dbContext = DbContext;
            Type masterType = dbContext.GetType().Assembly.GetTypes().First(t => t.Namespace == $"Models.Contexts.{this.DatasetName}" && t.Name == "Master");
            var masterIndexProperty = masterType.GetProperty("Index");
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
                var currentTypeIndexProperty = type.GetProperty("Index");

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
                                .OrderBy(masterIndexProperty.GetValue)
                                .DistinctBy(e =>
                                {
                                    var currentEntityIndex = masterIndexProperty.GetValue(e);

                                    var distinctProperties = e.GetType().GetProperties()
                                        .Where(p => distinctMasterPropertyNames
                                            .Contains(p.Name));

                                    return string.Join("", distinctProperties.Select(p => p.GetValue(e)));
                                }).Select(e =>
                                {
                                    var instance = Activator.CreateInstance(type);
                                    var currentEntityIndex = masterIndexProperty.GetValue(e);
                                    currentTypeIndexProperty.SetValue(instance, currentEntityIndex);
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
                IEnumerable<object> childRows = masterDbSet.OrderBy(masterIndexProperty.GetValue)
                                        .DistinctBy(e =>
                                        {

                                            var distinctProperties = e.GetType().GetProperties()
                                                .Where(p => distinctMasterPropertyNamesChild
                                                    .Contains(p.Name));


                                            return string.Join("", distinctProperties.Select(p => p.GetValue(e)));
                                        }).ToList();

                var totalChildrenCount = childRows.Count();
                var usedIndexes = new HashSet<int>();

                foreach (var parent in currentDbSet)
                {
                    var parentId = type.GetProperties().First(property => property.Name == type.Name + "Id").GetValue(parent);
                    var childIndexProperty = childType.GetProperties().First(prop => prop.Name == "Index");

                    childRows = childRows.Where(e => !usedIndexes.Contains((int)masterIndexProperty.GetValue(e)));
                    var children = childRows.Where((child, i) =>
                    {
                        // Filtering is fast. No significant optimization is possible.
                        var nextParent = parent;
                        while (nextParent != null)
                        {
                            foreach (var boundMasterProperty in boundMasterPropertiesChild)
                            {
                                var columnName = boundMasterProperty.GetCustomAttribute<BindToMasterAttribute>().MasterPropertyName;
                                try
                                {
                                    var parentProperty = boundMasterProperty;
                                    var childProperty = masterType.GetProperty(columnName);
                                    var parentValue = parentProperty.GetValue(nextParent);
                                    var childValue = childProperty.GetValue(child);
                                    if (childValue.ToString() != parentValue.ToString())
                                    {
                                        return false;
                                    }
                                }
                                catch
                                {
                                }

                            }
                            var nextParentLevel = nextParent.GetType().GetCustomAttribute<FilterAttribute>().Level;
                            string name = types.Skip((int)nextParentLevel - 1).FirstOrDefault()?.Name;
                            nextParent = (int)nextParentLevel == 0 ? null : nextParent.GetType().GetProperty(name).GetValue(nextParent);
                        }
                        return true;
                    }).Select((e, i) =>
                    {
                        var instance = Activator.CreateInstance(childType);
                        var currentEntityIndex = (int)masterIndexProperty.GetValue(e);
                        childIndexProperty.SetValue(instance, currentEntityIndex);
                        usedIndexes.Add(currentEntityIndex);
                        foreach (var boundProperty in boundMasterPropertiesChild)
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
                        childType.GetProperty(type.Name + "Id").SetValue(instance, parentId);
                        childType.GetProperty($"{childType.Name}Id").SetValue(instance, usedIndexes.Count());
                        return instance;
                    });

                    object firstChild = null;
                    foreach (var child in children)
                    {
                        if (firstChild == null)
                            firstChild = child;

                        Console.Write($"\rFound {usedIndexes.Count()} out of {totalChildrenCount} children...");

                        childDbSet.GetType().GetMethod("Add").Invoke(childDbSet, new object[] { child });
                    }

                    int? firstId = null;
                    try
                    {
                        firstId = (int)childType.GetProperty($"{childType.Name}Id").GetValue(firstChild);

                    }
                    finally
                    {
                        type.GetProperty($"Default{childType.Name}Id").SetValue(parent, firstId);

                    }
                }

                dbContext.SaveChanges();
                Console.WriteLine($"\rLoaded {childDbSet.Count()} {childType.Name} entities!");
            }

        }
    }
}