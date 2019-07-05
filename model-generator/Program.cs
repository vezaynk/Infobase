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
using Infobase.Models;
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
    public class DummyTypeMapper : IRelationalTypeMapper
    {
        public IByteArrayRelationalTypeMapper ByteArrayMapper { get; }
        public IStringRelationalTypeMapper StringMapper { get; }
        public RelationalTypeMapping FindMapping(Microsoft.EntityFrameworkCore.Metadata.IProperty property)
        {
            throw new NotImplementedException("This is a dummy");
        }
        public RelationalTypeMapping FindMapping(string storeType)
        {
            throw new NotImplementedException("This is a dummy");
        }
        public RelationalTypeMapping FindMapping(Type clrType)
        {
            throw new NotImplementedException("This is a dummy");
        }
        public void ValidateTypeName(string storeType)
        {
            throw new NotImplementedException("This is a dummy");
        }
        public bool IsTypeMapped(Type clrType)
        {
            throw new NotImplementedException("This is a dummy");
        }
    }
    public class Program
    {
        public static async Task Main(string[] args)
        {

            var imc = new InMemoryCompiler();
            // imc.AddFile("../infobase/Migrations/20190705145030_sefesrgfqE.cs");
            // imc.AddFile("../infobase/Migrations/20190705145030_sefesrgfqE.Designer.cs");
            // imc.AddFile("../infobase/Migrations/PASSContextModelSnapshot.cs");
            //var asm = imc.CompileAssembly();
            // Console.WriteLine(asm.GetName());
            // foreach (var t in asm.GetTypes())
            //     Console.WriteLine(t);
            Console.WriteLine(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
            var connectionstring = "Host=localhost;Port=5432;Database=phac_pass;Username=postgres;SslMode=Prefer;Trust Server Certificate=true;";

            var optionsBuilder = new DbContextOptionsBuilder<PASSContext>();
            optionsBuilder.UseNpgsql(connectionstring, o => o.MigrationsAssembly(imc.CompileAssembly().GetName().ToString()));

            // PASSContext dbContext = new PASSContext(optionsBuilder.Options);

            // //await dbContext.Database.MigrateAsync();
            // Console.WriteLine("Done!");

            //   var designTimeServiceCollection = new ServiceCollection()
            //                 .AddEntityFrameworkDesignTimeServices()
            //                 //.AddSingleton<MigrationsScaffolder>()
            //                 .AddDbContextDesignTimeServices(dbContext);
            //             new NpgsqlDesignTimeServices().ConfigureDesignTimeServices(designTimeServiceCollection);
            //             var sp = designTimeServiceCollection.BuildServiceProvider();
            //             var scaffolder = sp.GetRequiredService<MigrationsScaffolder>();
            //             var migration = scaffolder.ScaffoldMigration("MyMigration", "Infobase");
            //             Console.WriteLine(migration.MigrationCode);

            using (var mg = new model_generator2.MigrationGenerator<PASSContext>(optionsBuilder.Options))
            {
                var migration = mg.CreateMigration();

                var imc2 = new InMemoryCompiler();
                imc2.AddCodeBody(migration.SnapshotCode);
                imc2.AddCodeBody(migration.MigrationCode);
                imc2.AddCodeBody(migration.MetadataCode);
                imc2.CompileAssembly();

                var optionsBuilder2 = new DbContextOptionsBuilder<PASSContext>();
                optionsBuilder2.UseNpgsql(connectionstring, o => o.MigrationsAssembly(imc2.CompileAssembly().GetName().ToString()));

                using (var mg2 = new model_generator2.MigrationGenerator<PASSContext>(optionsBuilder2.Options))
                {

                    var migration2 = mg2.CreateMigration();

                    await mg2.DbContext.Database.MigrateAsync();
                    Console.WriteLine(migration2.MigrationCode);
                }
            }



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