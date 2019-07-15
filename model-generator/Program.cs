using System;
using System.Collections.Generic;
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

namespace model_generator
{
    public class Program
    {
        public static ScaffoldedMigration CreateMigration(DbContext dbContext)
        {
            var designTimeServiceCollection = new ServiceCollection()
                .AddEntityFrameworkDesignTimeServices()
                .AddDbContextDesignTimeServices(dbContext);
            new NpgsqlDesignTimeServices().ConfigureDesignTimeServices(designTimeServiceCollection);

            var designTimeServiceProvider = designTimeServiceCollection.BuildServiceProvider();

            var migrationsScaffolder = designTimeServiceProvider.GetService<IMigrationsScaffolder>();

            var migration = migrationsScaffolder.ScaffoldMigration(
                Path.GetRandomFileName(),
                "Infobase");

            return migration;
        }

        public static DbContext GetDBContext(Assembly dbContextASM, string dbContextFullName, Action<DbContextOptionsBuilder> configureOptionBuilder)
        {
            var dbContextRuntime = dbContextASM.GetType(dbContextFullName);
            Type genericOptionBuilder = typeof(DbContextOptionsBuilder<>).MakeGenericType(new Type[] { dbContextRuntime });
            var optionsBuilderDyn = Activator.CreateInstance(genericOptionBuilder);
            configureOptionBuilder(((DbContextOptionsBuilder)optionsBuilderDyn));
            var dbContextDyn = (DbContext)Activator.CreateInstance(dbContextRuntime, new object[] {
                    optionsBuilderDyn.GetType().BaseType.GetProperty("Options").GetValue(optionsBuilderDyn)
                });
            return dbContextDyn;
        }

        public static DbContext GetDBContextFromSource(string name, string path, Action<DbContextOptionsBuilder, Assembly> configureOptionBuilder, IEnumerable<ScaffoldedMigration> migrations = null, string migrationsDirectory = null)
        {
            // We can either load the assembly via compilation
            var dbContextIMC = new InMemoryCompiler();
            dbContextIMC.AddFile($"{path}/{name}Context.cs");
            foreach (var filename in Directory.GetFileSystemEntries($"{path}/{name}", "*.cs"))
            {
                dbContextIMC.AddFile(filename);
            }
            if (migrations != null)
            {
                foreach (var migration in migrations)
                {
                    dbContextIMC.AddCodeBody(migration.MetadataCode);
                    dbContextIMC.AddCodeBody(migration.MigrationCode);
                    dbContextIMC.AddCodeBody(migration.SnapshotCode);
                }
            }
            if (migrationsDirectory != null)
                foreach (string filename in Directory.GetFiles(migrationsDirectory, "*.cs"))
                {
                    dbContextIMC.AddFile(filename);
                }

            var asm = dbContextIMC.CompileAssembly();
            return GetDBContext(asm, $"Infobase.Models.{name}Context", ob => configureOptionBuilder(ob, asm));
        }

        public static async Task Main(string[] args)
        {
            var connectionstring = "Host=localhost;Port=5432;Database=phac_pass;Username=postgres;SslMode=Prefer;Trust Server Certificate=true;";
            var migrationsDirectory = "../infobase/Migrations/";

            Console.Write("Building DBContext from source...");
            var passdb = GetDBContextFromSource("PASS", "../infobase/Models", (ob, asm) => ob.UseNpgsql(connectionstring, o => o.MigrationsAssembly(asm.GetName().ToString())), migrationsDirectory: migrationsDirectory);
            Console.WriteLine("Created " + passdb.GetType().Name);

            Console.Write("Creating migration...");
            var migration = CreateMigration(passdb);
            Console.WriteLine($"Created migration (ID:{migration.MigrationId})");

            // Console.WriteLine("Writing migration files...");
            // Console.WriteLine($"Migration: {migrationsDirectory + migration.MigrationId + migration.FileExtension}");
            // File.WriteAllText(migrationsDirectory + migration.MigrationId + migration.FileExtension,
            //         migration.MigrationCode);

            // Console.WriteLine($"Designer: {migrationsDirectory + migration.MigrationId + ".Designer" + migration.FileExtension}");
            // File.WriteAllText(migrationsDirectory + migration.MigrationId + ".Designer" + migration.FileExtension,
            //     migration.MetadataCode);

            // Console.WriteLine($"Snapshot: {migrationsDirectory + migration.SnapshotName + migration.FileExtension}");
            // File.WriteAllText(migrationsDirectory + migration.SnapshotName + migration.FileExtension,
            //    migration.SnapshotCode);

            // Console.WriteLine("Done!");

            Console.Write("Rebuilding with migration...");
            var passdb2 = GetDBContextFromSource("PASS", "../infobase/Models", (ob, asm) => ob.UseNpgsql(connectionstring, o => o.MigrationsAssembly(asm.GetName().ToString())), new[] { migration }, migrationsDirectory);
            Console.WriteLine("Rebuilt!");

            Console.Write($"Migrating ({passdb2.Database.GetPendingMigrations().Count()} pending migrations)...");
            Console.Write("Cleaning...");

            await passdb2.Database.EnsureDeletedAsync();
            Console.Write("Applying...");

            await passdb2.Database.MigrateAsync();
            Console.WriteLine($"Done! Database has been updated to match the models.");


            var hi = passdb.Model.GetEntityTypes().First();
            var types = passdb.GetType().Assembly.GetTypes().Where(t => t.Namespace == "Infobase.Models.PASS" && t.Name == "Master").ToList();
            Type masterType = types.First();
            var dbSet = passdb.GetType().GetMethod("Set").MakeGenericMethod(new[] { masterType }).Invoke(passdb, new object[] {  });
            
                Console.WriteLine("Hello");
            using (var csv = new CsvReader(new StreamReader(@"./pass.csv"), new CsvHelper.Configuration.Configuration
            {
                Delimiter = ",",
                Encoding = Encoding.UTF8
            }))
            {
                Console.WriteLine("Hello");
                csv.Read();
                csv.ReadHeader();

                var records = csv.GetRecords<dynamic>();
                foreach (var record in records) {

                    var dict = (IDictionary<string, object>) record;
                    var masterInstance = Activator.CreateInstance(masterType);
                    
                    masterType.GetProperties()
                        .Where(p => p.GetCustomAttribute(typeof(CSVColumnAttribute)) != null)
                        .ToList()
                        .ForEach(p => {
                            string column = p.GetCustomAttribute<CSVColumnAttribute>().CSVColumnName;
                            dict.TryGetValue(column, out var value);
                            p.SetValue(masterInstance, value);
                        });

                    dbSet.GetType().GetMethod("Add").Invoke(dbSet, new object[]{ masterInstance });

                
                }

                passdb.SaveChanges();
                
                // foreach (string header in csv.Context.HeaderRecord)
                // {
                //     var row = Activator.CreateInstance(masterType);
                //     row.GetType().GetProperty("Activity").SetValue(row, "Hello World");
                //     dbSet.GetType().GetMethod("Add").Invoke(dbSet, new object[]{ row });
                //     passdb.SaveChanges();
                // }


                try
                {
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
                }
                catch (System.Exception e)
                {
                    Console.Write(e);
                }

            }
        }

        public static string ToPascalCase(string text)
        {
            return new string(text.Split(new[] { "_", " ", "-" }, StringSplitOptions.RemoveEmptyEntries).Select(s => char.ToUpperInvariant(s[0]) + s.Substring(1, s.Length - 1)).Aggregate(string.Empty, (s1, s2) => s1 + s2).Where(c => char.IsLetterOrDigit(c)).ToArray());
        }
    }
}