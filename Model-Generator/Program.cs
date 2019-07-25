﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using RazorLight;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Model_Generator
{
    public class Program
    {
        public static void SetupDatabase(bool buildFromSource)
        {
            string datasetName = "PASS";
            string csvFilePath = "./pass.csv";
            var connectionString = $"Host=localhost;Port=5432;Database={datasetName};Username=postgres;SslMode=Prefer;Trust Server Certificate=true;";

            DatabaseCreator databaseCreator;

            if (buildFromSource)
            {
                var migrationsDirectory = "../models/Migrations/";
                var modelsDirectory = "../models/Contexts/";
                Console.Write("Building DBContext from source...");
                databaseCreator = new DatabaseCreator(connectionString, migrationsDirectory, modelsDirectory, datasetName);
                Console.WriteLine("Created " + databaseCreator.DbContext.GetType().Name);
                databaseCreator.CreateMigration();
                Console.Write("Rebuilding...");
                databaseCreator.ReloadDbContext();
                Console.WriteLine("Rebuilt!");
                databaseCreator.SaveMigrations();
            }
            else
            {
                Assembly.LoadFrom(Path.GetFullPath("../infobase/bin/Debug/netcoreapp2.2/models.dll"));
                databaseCreator = new DatabaseCreator(connectionString, Path.GetFullPath("../infobase/bin/Debug/netcoreapp2.2/models.dll"), datasetName);
            }

            Console.Write("Preparing Database...");
            Console.Write("Cleaning...");
            databaseCreator.CleanDatabase();
            databaseCreator.ReloadDbContext();
            
            Console.Write($"Migrating ({databaseCreator.DbContext.Database.GetPendingMigrations().Count()} pending migrations)...");
            databaseCreator.ApplyMigrations();
            Console.WriteLine($"Done! Database has been updated to match the models.");

            using (var sr = new StreamReader(csvFilePath))
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
            // var x = typeof(Models.{datasetName}.Activity);
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
    
}