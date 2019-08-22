using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Text.Encodings;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using RazorLight;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using CsvHelper.Configuration;
using CsvHelper;
using CSharpLoader;
using Models.Metadata;
using CommandLine;

namespace Model_Generator
{
    public class Options
    {
        [Option('l', "load", Required = false, HelpText = "Set if you want data to be loaded into the database", Default=true)]
        public bool LoadData { get; set; }
        [Option('c', "create", Required = false, HelpText = "Set if you want new models to be created for this dataset", Default=false)]
        public bool CreateModels { get; set; }

        [Option('d', "dataset", Required = true, HelpText = "Set the name of the dataset to operate on")]
        public string Dataset { get; set; }

        [Option('f', "file", Required = true, HelpText = "Set the path to the CSV to use")]
        public string File { get; set; }
    }
    public class Program
    {
        public static bool IsCodeValid(string code)
        {
            try
            {
                var imc = new InMemoryCompiler();
                imc.AddCodeBody(code);
                // Validate code
                imc.CompileAssembly();
                return true;
            }
            catch
            {
                return false;
            }

        }
        public static string GenerateMasterSource(string datasetName, IEnumerable<string> headers)
        {
            var engine = new RazorLightEngineBuilder()
                            .UseFilesystemProject(Path.GetFullPath("./Templates"))
                            .UseMemoryCachingProvider()
                            .Build();

            var outputMaster = engine.CompileRenderAsync("MasterEntity.cshtml", new MasterEntityModel
            {
                DatasetName = datasetName,
                Properties = headers
            }).GetAwaiter().GetResult();

            return outputMaster;
        }
        public static string GenerateContextSource(string datasetName, IEnumerable<string> headers)
        {
            var engine = new RazorLightEngineBuilder()
                            .UseFilesystemProject(Path.GetFullPath("./Templates"))
                            .UseMemoryCachingProvider()
                            .Build();

            var outputContext = engine.CompileRenderAsync("ContextEntity.cshtml", new MasterEntityModel
            {
                DatasetName = datasetName,
                Properties = headers
            }).GetAwaiter().GetResult();

            return outputContext;
        }
        public static string GenerateModelsForMaster(Type masterType)
        {
            var engine = new RazorLightEngineBuilder()
                            .UseFilesystemProject(Path.GetFullPath("./Templates"))
                            .UseMemoryCachingProvider()
                            .Build();
            var outputModels = engine.CompileRenderAsync("ModelsEntity.cshtml", masterType).GetAwaiter().GetResult();

            return outputModels;

        }
        public static void SetupDatabase(string datasetName,
                                            string csvFilePath,
                                            string connectionString, bool migrate)
        {
            var databaseCreator = new DatabaseCreator(connectionString, datasetName, migrate ? null : typeof(Models.Metadata.Metadata).Assembly);

            Console.WriteLine("Created " + databaseCreator.DbContext.GetType().Name);

            if (migrate)
            {
                databaseCreator.CreateMigration(datasetName);
                databaseCreator.SaveMigrations();
            }

            Console.Write("Preparing Database...");
            Console.Write("Cleaning...");
            databaseCreator.CleanDatabase();

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

        public static async Task Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                   .WithParsed<Options>(o =>
                   {
                       bool generateModels = o.CreateModels;
                       bool loadData = o.LoadData;

                       Console.Write("Enter dataset name: ");
                       string datasetName = o.Dataset.ToUpper();
                       Console.Write("Enter dataset file path: ");
                       string csvFilePath = o.File;

                       var connectionString = $"Host=localhost;Port=5432;Database={datasetName};Username=postgres;SslMode=Prefer;Trust Server Certificate=true;";



                       if (generateModels)
                       {
                           Console.WriteLine("This process will guide you through the creation of the data tool. Press enter to continue or ^C to exit.");
                           Console.Read();
                           using (var sr = new StreamReader(csvFilePath))
                           using (var csv = new CsvReader(sr, new Configuration
                           {
                               Delimiter = ",",
                               Encoding = Encoding.UTF8
                           }))
                           {
                               csv.Read();
                               csv.ReadHeader();

                               var directory = new DirectoryInfo($"../Models/Contexts/{datasetName}");
                               if (directory.Exists)
                               {
                                   Console.WriteLine($"{directory.FullName} directory already exists. Manually delete it or use another name for the dataset");
                                   Environment.Exit(0);
                               }

                               Console.Write("Generating master entity");
                               var outputMaster = GenerateMasterSource(datasetName, csv.Context.HeaderRecord);
                               bool isValidMaster = IsCodeValid(outputMaster);
                               if (!isValidMaster)
                               {
                                   throw new Exception("Failed to generate valid master file");
                               }

                               string masterPath = Path.Join(directory.FullName, "Master.cs");
                               File.WriteAllText(masterPath, outputMaster);
                               var editedMasterValidated = false;
                               while (!editedMasterValidated)
                               {
                                   Console.WriteLine($"Created {masterPath}. Press Enter when done editing.");
                                   Console.ReadLine();
                                   outputMaster = File.ReadAllText(masterPath);
                                   editedMasterValidated = IsCodeValid(outputMaster);
                               }

                               var imc = new InMemoryCompiler();
                               imc.AddCodeBody(outputMaster);
                               var masterType = imc.CompileAssembly().GetType($"Models.Contexts.{datasetName}.Master");


                               Console.Write("Generating model entities");
                               var outputModels = GenerateModelsForMaster(masterType);
                               bool isValidModels = IsCodeValid(outputModels);
                               if (!isValidModels)
                               {
                                   throw new Exception("Failed to generate valid models file");
                               }


                               string modelsPath = Path.Join(directory.FullName, "Models.cs");

                               File.WriteAllText(modelsPath, outputModels);
                               var editedModelsValidated = false;
                               while (!editedModelsValidated)
                               {
                                   Console.WriteLine($"Created {modelsPath}. Press Enter when done editing.");
                                   Console.ReadLine();
                                   outputModels = File.ReadAllText(modelsPath);
                                   editedModelsValidated = IsCodeValid(outputModels);
                               }

                               imc = new InMemoryCompiler();
                               imc.AddCodeBody(outputModels);
                               var names = imc.CompileAssembly().GetTypes().Where(t => t.Namespace == $"Models.Contexts.{datasetName}").Select(t => t.Name);
                               var outputContext = GenerateContextSource(datasetName, names);
                               bool isValidContext = IsCodeValid(outputContext);
                               if (!isValidContext)
                               {
                                   throw new Exception("Failed to generate valid models file");
                               }

                               string contextPath = Path.Join(directory.FullName, "Context.cs");
                               File.WriteAllText(contextPath, outputContext);
                           }
                       }

                       Console.WriteLine("Done. Confirm results and press enter to begin database initialization.");
                       // Console.ReadLine();

                       SetupDatabase(datasetName, csvFilePath, connectionString, generateModels);
                   });

        }


        public static string ToPascalCase(string text)
        {
            return new string(text.Split(new[] { "_", " ", "-" }, StringSplitOptions.RemoveEmptyEntries).Select(s => char.ToUpperInvariant(s[0]) + s.Substring(1, s.Length - 1)).Aggregate(string.Empty, (s1, s2) => s1 + s2).Where(c => char.IsLetterOrDigit(c)).ToArray());
        }
    }

}