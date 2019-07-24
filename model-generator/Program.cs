using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Text;
using System.Collections.ObjectModel;
using metadata_annotations;
using CSharpLoader;
using CsvHelper;
using RazorLight;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Design;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Storage;
using Npgsql.EntityFrameworkCore.PostgreSQL.Design.Internal;
using Microsoft.EntityFrameworkCore.Proxies;
using StackExchange.Profiling;

namespace model_generator
{
    public class MigrationGenerator
    {
        public DbContext DbContext { get; set; }
        public string MigrationName { get; set; }
        public MigrationGenerator(DbContext dbContext, string migrationName = null)
        {
            DbContext = dbContext;
            // Use a random filename if none is specified
            MigrationName = migrationName ?? Path.GetRandomFileName();
        }
        public ScaffoldedMigration CreateMigration()
        {
            // Extract necessary objects to perform migration
            var designTimeServiceCollection = new ServiceCollection()
                .AddEntityFrameworkDesignTimeServices()
                .AddDbContextDesignTimeServices(DbContext);

            new NpgsqlDesignTimeServices().ConfigureDesignTimeServices(designTimeServiceCollection);

            var designTimeServiceProvider = designTimeServiceCollection.BuildServiceProvider();

            var migrationsScaffolder = designTimeServiceProvider.GetService<IMigrationsScaffolder>();

            // This builds the *incrementental migration* to achieve parity with schema
            var migration = migrationsScaffolder.ScaffoldMigration(
                MigrationName,
                "Infobase");

            return migration;
        }
    }
    public static class DbContextBuilder
    {
        public static DbContext GetDBContext(Assembly dbContextASM, string dbContextFullName, Action<DbContextOptionsBuilder> configureOptionBuilder)
        {
            // Get type of the dbContext. This is needed to use any generic methods involving a dbContexts
            Type dbContextType = dbContextASM.GetType(dbContextFullName);
            // Generic OptionBuilder type to work with the loaded DbContext 
            Type optionBuilderType = typeof(DbContextOptionsBuilder<>).MakeGenericType(new Type[] { dbContextType });
            // Instance of optionBuilder
            DbContextOptionsBuilder optionsBuilder = (DbContextOptionsBuilder)Activator.CreateInstance(optionBuilderType);

            // Let caller configure it
            configureOptionBuilder(optionsBuilder);

            // Create dbContext using configured optionBuilder
            var dbContext = (DbContext)Activator.CreateInstance(dbContextType, new object[] {
                    optionsBuilder.Options
                });

            return dbContext;
        }

        public static DbContext GetDBContextFromSource(string name, string path, Action<DbContextOptionsBuilder, Assembly> configureOptionBuilder, IEnumerable<ScaffoldedMigration> migrations = null, string migrationsDirectory = null)
        {

            var asm = BuildAssembly(name, path, migrations, migrationsDirectory);
            // Extract DbContext from the resulting assembly
            return GetDBContext(asm, $"Infobase.Models.{name}Context", ob => configureOptionBuilder(ob, asm));
        }

        public static Assembly BuildAssembly(string name, string path, IEnumerable<ScaffoldedMigration> migrations = null, string migrationsDirectory = null)
        {
            var dbContextIMC = new InMemoryCompiler();

            // The Context file is the main file that is analyzed in order to determine what needs to be migrated.
            // In fact, the actual migration files are not necessary to generate migrations.
            // They are, however, necessary to apply them to the database.
            dbContextIMC.AddFile($"{Path.Join(path, name)}Context.cs");

            // The actual entities are stored in ModelsFolder/NameOfDataset
            foreach (var filename in Directory.GetFileSystemEntries($"{Path.Join(path, name)}", "*.cs"))
            {
                dbContextIMC.AddFile(filename);
            }

            // Sometimes, its convenient to load in a migration without saving it to disk
            if (migrations != null)
            {
                foreach (var migration in migrations)
                {
                    // Not sure what it does, but it comes with the others
                    dbContextIMC.AddCodeBody(migration.MetadataCode);
                    // Necessary to apply migration
                    dbContextIMC.AddCodeBody(migration.MigrationCode);
                    // Necessary to generate new migrations
                    dbContextIMC.AddCodeBody(migration.SnapshotCode);
                }
            }

            // If there is a known migration directory, we should load all of them
            if (migrationsDirectory != null)
                foreach (string filename in Directory.GetFiles(migrationsDirectory, "*.cs"))
                {
                    dbContextIMC.AddFile(filename);
                }

            return dbContextIMC.CompileAssembly();
        }

    }
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

        }
        private DatabaseCreator(string connectionString, Assembly assembly, string datasetName)
        {
            this.DbContext = DbContextBuilder.GetDBContext(assembly, $"Infobase.Models.{datasetName}Context", ob => ob.UseNpgsql(connectionString, o => o.MigrationsAssembly(assembly.GetName().ToString())));
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
            var mg = new MigrationGenerator(DbContext, DatasetName);
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
            // Load PASS Master specifically
            Type masterType = dbContext.GetType().Assembly.GetTypes().First(t => t.Namespace == "Infobase.Models.PASS" && t.Name == "Master");

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
            Type masterType = dbContext.GetType().Assembly.GetTypes().First(t => t.Namespace == "Infobase.Models.PASS" && t.Name == "Master");
            var masterIndexProperty = masterType.GetProperty("Index");
            var masterDbSet = GetDbSet(masterType);

            var types = dbContext.GetType().Assembly.GetTypes()
                // Load all PASS models, excluding the non-filter ones (Only the Master is excluded).
                .Where(t => t.Namespace == "Infobase.Models.PASS" && t.GetCustomAttribute<FilterAttribute>() != null)
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
                var fromCsvColumnsChild = types.Take(currentLevel + 2).SelectMany(t => t.GetProperties())
                    .Where(p => p.GetCustomAttribute<CSVColumnAttribute>() != null);

                // Get all the actual column names
                var distinctColumnNamesChild = fromCsvColumnsChild
                        .Select(p => p.GetCustomAttribute<CSVColumnAttribute>().CSVColumnName);

                // Get the dbset for the current and its child
                var currentDbSet = GetDbSet(type);
                var childDbSet = GetDbSet(childType);

                // As the key to this working is iterating through each parent and finding their children, the first filter must be loaded earlier
                if (type == types.First())
                {
                    Console.Write("Loading top-level entities...");
                    var fromCsvColumns = types.Take(currentLevel + 1).SelectMany(t => t.GetProperties())
                        .Where(p => p.GetCustomAttribute<CSVColumnAttribute>() != null);
                    var distinctColumnNames = fromCsvColumns
                            .Select(p => p.GetCustomAttribute<CSVColumnAttribute>().CSVColumnName);
                    var topLevelRows = masterDbSet
                                .OrderBy(masterIndexProperty.GetValue)
                                .DistinctBy(e =>
                                {
                                    var currentEntityIndex = masterIndexProperty.GetValue(e);

                                    var distinctProperties = e.GetType().GetProperties()
                                        .Where(p => distinctColumnNames
                                            .Contains(p.GetCustomAttribute<CSVColumnAttribute>()?.CSVColumnName));

                                    return string.Join("", distinctProperties.Select(p => p.GetValue(e)));
                                }).Select(e =>
                                {
                                    var instance = Activator.CreateInstance(type);
                                    var currentEntityIndex = masterIndexProperty.GetValue(e);
                                    currentTypeIndexProperty.SetValue(instance, currentEntityIndex);
                                    foreach (var p in e.GetType().GetProperties())
                                    {
                                        foreach (var column in fromCsvColumns)
                                        {
                                            if (p.GetCustomAttribute<CSVColumnAttribute>()?.CSVColumnName == column.GetCustomAttribute<CSVColumnAttribute>().CSVColumnName)
                                            {
                                                try
                                                {
                                                    column.SetValue(instance, p.GetValue(e));
                                                }
                                                catch
                                                {

                                                }

                                            }
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
                                                .Where(p => distinctColumnNamesChild
                                                    .Contains(p.GetCustomAttribute<CSVColumnAttribute>()?.CSVColumnName));


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
                            foreach (string columnName in distinctColumnNamesChild)
                            {
                                try
                                {
                                    var parentProperty = nextParent.GetType().GetProperties().First(pt => pt.GetCustomAttribute<CSVColumnAttribute>()?.CSVColumnName == columnName);
                                    var childProperty = masterType.GetProperties().First(pt => pt.GetCustomAttribute<CSVColumnAttribute>()?.CSVColumnName == columnName);
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
                    }).Select(e =>
                    {
                        var instance = Activator.CreateInstance(childType);
                        var currentEntityIndex = (int)masterIndexProperty.GetValue(e);
                        childIndexProperty.SetValue(instance, currentEntityIndex);
                        usedIndexes.Add(currentEntityIndex);
                        foreach (var p in e.GetType().GetProperties())
                        {
                            foreach (var column in fromCsvColumnsChild)
                            {
                                if (p.GetCustomAttribute<CSVColumnAttribute>()?.CSVColumnName == column.GetCustomAttribute<CSVColumnAttribute>().CSVColumnName)
                                {
                                    try
                                    {
                                        column.SetValue(instance, p.GetValue(e));
                                    }
                                    catch
                                    {

                                    }

                                }
                            }
                        }
                        childType.GetProperties().First(property => property.Name == type.Name + "Id").SetValue(instance, parentId);

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
    public class Program
    {
        public static void SetupDatabase(bool buildFromSource)
        {
            var connectionString = "Host=localhost;Port=5432;Database=phac_pass;Username=postgres;SslMode=Prefer;Trust Server Certificate=true;";

            DatabaseCreator databaseCreator;

            if (buildFromSource)
            {
                var migrationsDirectory = "../infobase/Migrations/";
                var modelsDirectory = "../infobase/Models";
                Console.Write("Building DBContext from source...");
                databaseCreator = new DatabaseCreator(connectionString, migrationsDirectory, modelsDirectory, "PASS");
                Console.WriteLine("Created " + databaseCreator.DbContext.GetType().Name);
                databaseCreator.CreateMigration();
                Console.Write("Rebuilding...");
                databaseCreator.ReloadDbContext();
                Console.WriteLine("Rebuilt!");
                databaseCreator.SaveMigrations();
            }
            else
            {
                Assembly.LoadFrom(Path.GetFullPath("../infobase/bin/Release/netcoreapp2.2/publish/Infobase.dll"));
                databaseCreator = new DatabaseCreator(connectionString, Path.GetFullPath("../infobase/bin/Release/netcoreapp2.2/publish/Infobase.dll"), "PASS");
            }

            Console.Write("Preparing Database...");
            Console.Write("Cleaning...");
            databaseCreator.CleanDatabase();
            databaseCreator.ReloadDbContext();
            
            Console.Write($"Migrating ({databaseCreator.DbContext.Database.GetPendingMigrations().Count()} pending migrations)...");
            databaseCreator.ApplyMigrations();
            Console.WriteLine($"Done! Database has been updated to match the models.");

            using (var sr = new StreamReader(@"./pass.csv"))
            {
                Console.Write($"Loading all rows from file into Master table ({sr.BaseStream.Length} bytes)...");
                int rowsLoaded = databaseCreator.LoadMasterCSV(sr);
                Console.WriteLine($"Loaded {rowsLoaded} rows into Database!");
            }
            databaseCreator.LoadEntitiesFromMaster();
        }
        public static void Main(string[] args)
        {
            var lf = new LoggerFactory();
            var l = lf.CreateLogger(typeof(DbContext));
            l.LogInformation("Test");
            SetupDatabase(false);

            // try
            // {
            // var engine = new RazorLightEngineBuilder()
            //                 .UseFilesystemProject($"{Directory.GetCurrentDirectory()}/Templates")
            //                 .UseMemoryCachingProvider()
            //                 .Build();

            // var output = await engine.CompileRenderAsync("MasterEntity.cshtml", new MasterEntityModel
            // {
            //     DatasetName = "PASS",
            //     Properties = csv.Context.HeaderRecord
            // });
            // Console.WriteLine(output);
            // var imc = new InMemoryCompiler();
            // imc.AddCodeBody(output);
            // var asm = imc.CompileAssembly();
            // Console.WriteLine(output);
            //Console.WriteLine(await engine.CompileRenderAsync("ImportSQL.cshtml", models));

            // foreach (var a in models)
            // {

            //string result = await engine.CompileRenderAsync("Entity.cshtml", a);
            //Console.WriteLine(a);
            // var childAttribute = a.GetCustomAttribute<ChildOf>();
            // if (childAttribute == null)
            // {
            //     Console.WriteLine("Null");
            // }
            // else
            // {
            //     Console.WriteLine($"Parent Name: {childAttribute.Parent.Name}");
            // }
            // var parentAttribute = a.GetCustomAttribute<ParentOf>();
            // if (parentAttribute == null)
            // {
            //     Console.WriteLine("Null");
            // }
            // else
            // {
            //     Console.WriteLine($"Child Name: {parentAttribute.Child.Name}");
            // }

            // var textDataAttributes = a.GetCustomAttributes<TextData>();
            // foreach (var textDataAttribute in textDataAttributes)
            // {
            //     Console.WriteLine($"Text Name: {textDataAttribute.Name}");
            // }


            // var modifierAttribute = a.GetCustomAttribute<Modifier>();
            // if (modifierAttribute == null)
            // {
            //     Console.WriteLine("Null");
            // }
            // else
            // {
            //     Console.WriteLine($"Modifiers: {modifierAttribute}");
            // }

            //     // modifierAttribute.Modifiers.HasFlag(ModelModifier.Aggregator)

            // }
            // var x = typeof(Models.PASS.Activity);
            // }
            // catch (System.Exception e)
            // {
            //     Console.Write(e);
            // }
        }


        public static string ToPascalCase(string text)
        {
            return new string(text.Split(new[] { "_", " ", "-" }, StringSplitOptions.RemoveEmptyEntries).Select(s => char.ToUpperInvariant(s[0]) + s.Substring(1, s.Length - 1)).Aggregate(string.Empty, (s1, s2) => s1 + s2).Where(c => char.IsLetterOrDigit(c)).ToArray());
        }


    }
    public static class LinqExtensions
    {
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>
    (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}