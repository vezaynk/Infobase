﻿using System;
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
// using Infobase.Models;
using Microsoft.EntityFrameworkCore.Migrations.Design;
using Microsoft.EntityFrameworkCore.Design.Internal;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;
using Npgsql.EntityFrameworkCore.PostgreSQL.Design.Internal;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System.Diagnostics;
using Npgsql.EntityFrameworkCore.PostgreSQL.Update.Internal;
using Microsoft.EntityFrameworkCore.Update;

namespace model_generator
{
    public class Program
    {
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

        public static DbContext GetDBContextFromSource(string name, string path, Action<DbContextOptionsBuilder> configureOptionBuilder)
        {
            // We can either load the assembly via compilation
            var dbContextIMC = new InMemoryCompiler();
            dbContextIMC.AddFile($"{path}/{name}Context.cs");
            foreach (var filename in Directory.GetFileSystemEntries($"{path}/{name}", "*.cs"))
            {
                dbContextIMC.AddFile(filename);
            }
            return GetDBContext(dbContextIMC.CompileAssembly(), $"Infobase.Models.{name}Context", configureOptionBuilder);
        }

        public static void Main(string[] args)
        {
            var connectionstring = "Host=localhost;Port=5432;Database=phac_pass;Username=postgres;SslMode=Prefer;Trust Server Certificate=true;";


            // // We can either load the assembly via compilation
            // GetDBContextFromSource("PASS", "../infobase/Models", dcob => dcob.UseNpgsql(connectionstring));
            // var dbContextASMAlt = dbContextIMC.CompileAssembly();

            // Or directly if its compiled already
            var dbContextASM = Assembly.LoadFile(Path.GetFullPath("../infobase/bin/Debug/netcoreapp2.2/Infobase.dll"));//
            var dbContextRuntime = dbContextASM.GetType("Infobase.Models.PASSContext");

            var dbContextDyn = GetDBContext(dbContextASM, "Infobase.Models.PASSContext", dcob => dcob.UseNpgsql(connectionstring));

            // var optionsBuilder = new DbContextOptionsBuilder<dbContext>();
            // optionsBuilder.UseNpgsql(connectionstring);

            var mg = new model_generator.MigrationGenerator(dbContextDyn);
            var migration = mg.CreateMigration();

            var migrationIMC = new InMemoryCompiler();
            migrationIMC.AddCodeBody(migration.SnapshotCode);
            migrationIMC.AddCodeBody(migration.MigrationCode);
            migrationIMC.AddCodeBody(migration.MetadataCode);
            //migrationIMC.CompileAssembly();

            var dbContextDyn2 = GetDBContext(dbContextASM, "Infobase.Models.PASSContext", dcob => dcob.UseNpgsql(connectionstring, o => o.MigrationsAssembly(migrationIMC.CompileAssembly().GetName().ToString())));

            // var optionsBuilder2 = new DbContextOptionsBuilder<dbContext>();
            // optionsBuilder2.UseNpgsql(connectionstring, o => o.MigrationsAssembly(migrationIMC.CompileAssembly().GetName().ToString()));

            var db = dbContextDyn2.Database;
            db.EnsureDeleted();
            db.Migrate();



            // using (var csv = new CsvReader(new StreamReader(@"./pass.csv"), new CsvHelper.Configuration.Configuration
            // {
            //     Delimiter = ",",
            //     Encoding = Encoding.UTF8
            // }))
            // {

            //     csv.Read();
            //     csv.ReadHeader();

            //     // foreach (string header in csv.Context.HeaderRecord)
            //     // {
            //     //     Console.WriteLine("Original: " + header);
            //     //     Console.WriteLine("Pascal: " + ToPascalCase(header));
            //     //     Console.WriteLine();
            //     // }


            //     try
            //     {
            //         var engine = new RazorLightEngineBuilder()
            //                         .UseFilesystemProject($"{Directory.GetCurrentDirectory()}/Templates")
            //                         .UseMemoryCachingProvider()
            //                         .Build();

            //         var output = await engine.CompileRenderAsync("MasterEntity.cshtml", new MasterEntityModel
            //         {
            //             DatasetName = "PASS",
            //             Properties = csv.Context.HeaderRecord
            //         });
            //         var imc = new InMemoryCompiler();
            //         imc.AddCodeBody(output);
            //         var asm = imc.CompileAssembly();
            //         Console.WriteLine(output);
            //         //Console.WriteLine(await engine.CompileRenderAsync("ImportSQL.cshtml", models));

            //         // foreach (var a in models)
            //         // {

            //         //string result = await engine.CompileRenderAsync("Entity.cshtml", a);
            //         //Console.WriteLine(a);
            //         // var childAttribute = a.GetCustomAttribute<ChildOf>();
            //         // if (childAttribute == null)
            //         // {
            //         //     Console.WriteLine("Null");
            //         // }
            //         // else
            //         // {
            //         //     Console.WriteLine($"Parent Name: {childAttribute.Parent.Name}");
            //         // }
            //         // var parentAttribute = a.GetCustomAttribute<ParentOf>();
            //         // if (parentAttribute == null)
            //         // {
            //         //     Console.WriteLine("Null");
            //         // }
            //         // else
            //         // {
            //         //     Console.WriteLine($"Child Name: {parentAttribute.Child.Name}");
            //         // }

            //         // var textDataAttributes = a.GetCustomAttributes<TextData>();
            //         // foreach (var textDataAttribute in textDataAttributes)
            //         // {
            //         //     Console.WriteLine($"Text Name: {textDataAttribute.Name}");
            //         // }


            //         // var modifierAttribute = a.GetCustomAttribute<Modifier>();
            //         // if (modifierAttribute == null)
            //         // {
            //         //     Console.WriteLine("Null");
            //         // }
            //         // else
            //         // {
            //         //     Console.WriteLine($"Modifiers: {modifierAttribute}");
            //         // }

            //         //     // modifierAttribute.Modifiers.HasFlag(ModelModifier.Aggregator)

            //         // }
            //         // var x = typeof(Models.PASS.Activity);
            //     }
            //     catch (System.Exception e)
            //     {
            //         Console.Write(e);
            //     }

            // }

            // // var csmemcompiler = new InMemoryCompiler(new[] {"./Test.cs", "../infobase/Models/PASS/Activity.cs"}, "/opt/dotnet/shared/Microsoft.NETCore.App/2.2.3/");
            // // Type loadedWriterType = csmemcompiler.GetType("RoslynCompileSample.Writer");
            // // var textProperty = loadedWriterType.GetProperty("MyProperty").GetCustomAttribute<TextProperty>();
            // // Console.WriteLine(textProperty.Name + " " + textProperty.Culture);

            // // //var instance = (RoslynCompileSample.Writer)Activator.CreateInstance(loadedWriterType);

            // // var instance = Activator.CreateInstance(loadedWriterType);
            // // var meth = loadedWriterType.GetMember("Write").First() as MethodInfo;
            // // meth.Invoke(instance, new[] { "Hello World" });
        }

        public static string ToPascalCase(string text)
        {
            return new string(text.Split(new[] { "_", " ", "-" }, StringSplitOptions.RemoveEmptyEntries).Select(s => char.ToUpperInvariant(s[0]) + s.Substring(1, s.Length - 1)).Aggregate(string.Empty, (s1, s2) => s1 + s2).Where(c => char.IsLetterOrDigit(c)).ToArray());
        }
    }
}