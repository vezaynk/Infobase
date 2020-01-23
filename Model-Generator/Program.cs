using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using RazorLight;
using Microsoft.EntityFrameworkCore;
using CsvHelper.Configuration;
using CsvHelper;
using CSharpLoader;
using CommandLine;

namespace Model_Generator
{
    public class Options
    {
        [Option('l', "load", Required = false, HelpText = "Set if you want data to be loaded into the database", Default = true)]
        public bool LoadData { get; set; }
        [Option('g', "generate", Required = false, HelpText = "Set if you want new models to be generated for this dataset", Default = false)]
        public bool GenerateModels { get; set; }

        [Option('d', "dataset", Required = true, HelpText = "Set the name of the dataset to operate on")]
        public string Dataset { get; set; }

        [Option('f', "file", Required = true, HelpText = "Set the path to the CSV to use")]
        public string File { get; set; }

        [Option('c', "connection", Required = true, HelpText = "Set connection string to use for the database")]
        public string ConnectionString { get; set; }

        [Option('t', "translatefrench", Required = false, HelpText = "Set french file to use for translations")]
        public string French { get; set; }
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
        public static void SetupDatabase(string datasetName, string csvFilePath, string connectionString, bool createFromSource, string frenchFile)
        {
            var databaseCreator = new DatabaseCreator(connectionString, datasetName, createFromSource ? null : typeof(Models.Metadata.Metadata).Assembly);

            Console.WriteLine("Created " + databaseCreator.DbContext.GetType().Name);

            Console.Write("Preparing Database...");
            Console.Write("Cleaning...");
            databaseCreator.CleanDatabase();

            Console.Write($"Creating...");
            databaseCreator.CreateDatabase();
            Console.WriteLine($"Done! Database has been updated to match the models.");

            using (var sr = new StreamReader(csvFilePath))
            {
                Console.Write($"Loading all rows from file into Master table ({sr.BaseStream.Length} bytes)...");
                int rowsLoaded = databaseCreator.LoadMasterCSV(sr);
                Console.WriteLine($"Loaded {rowsLoaded} rows into Database!");
            }


            if (frenchFile != null)
            {
                var translations = new Dictionary<string, string>();
                using (var reader = new StreamReader(frenchFile))
                using (var csv = new CsvReader(reader))
                {
                    csv.Configuration.HasHeaderRecord = false;
                    while (csv.Read())
                    {

                        csv.TryGetField(0, out string english);
                        csv.TryGetField(1, out string french);
                        translations.Add(english, french);
                    }
                    var missingTranslations = databaseCreator.LoadEntitiesFromMaster(translations);
                    foreach (var missingTranslation in missingTranslations) {
                        translations.Add(missingTranslation ?? "", null);
                    }
                }

                using (var writer = new StreamWriter(frenchFile, false))
                using (var csv = new CsvWriter(writer)) {
                    csv.Configuration.HasHeaderRecord = false;
                    foreach (var translation in translations) {
                        csv.WriteRecord(translation);
                        csv.NextRecord();
                    }
                }
            }
            else
            {
                databaseCreator.LoadEntitiesFromMaster();
            }


        }

        public static async Task Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                   .WithParsed<Options>(o =>
                   {
                       bool generateModels = o.GenerateModels;
                       bool loadData = o.LoadData;
                       string frenchFile = o.French;
                       bool loadFrench = !string.IsNullOrEmpty(frenchFile);

                       string datasetName = o.Dataset.ToUpper();
                       string csvFilePath = o.File;

                       var connectionString = o.ConnectionString;



                       if (generateModels)
                       {
                           Console.Write("This process will guide you through the creation of the data tool. Press enter to continue or ^C to exit.");
                           Console.ReadLine();
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
                               } else {
                                   directory.Create();
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
                                   Console.Write($"Created {masterPath}. Press Enter when done editing.");
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
                                   Console.Write($"Created {modelsPath}. Press Enter when done editing.");
                                   Console.ReadLine();
                                   outputModels = File.ReadAllText(modelsPath);
                                   editedModelsValidated = IsCodeValid(outputModels);
                               }

                               imc = new InMemoryCompiler();
                               imc.AddCodeBody(outputModels);
                               var names = imc.CompileAssembly().GetTypes().Where(t => t.Namespace == $"Models.Contexts.{datasetName}").Select(t => t.Name);
                               var outputContext = GenerateContextSource(datasetName, names);

                               string contextPath = Path.Join(directory.FullName, "Context.cs");
                               File.WriteAllText(contextPath, outputContext);
                           }
                       }

                       Console.WriteLine("Done. Confirm results and press enter to begin database initialization.");
                       Console.ReadLine();

                       if (loadData)
                           SetupDatabase(datasetName, csvFilePath, connectionString, generateModels, frenchFile);
                   });

        }


        public static string ToPascalCase(string text)
        {
            return new string(text.Split(new[] { "_", " ", "-" }, StringSplitOptions.RemoveEmptyEntries).Select(s => char.ToUpperInvariant(s[0]) + s.Substring(1, s.Length - 1)).Aggregate(string.Empty, (s1, s2) => s1 + s2).Where(c => char.IsLetterOrDigit(c)).ToArray());
        }
    }

}