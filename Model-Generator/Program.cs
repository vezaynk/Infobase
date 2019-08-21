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

namespace Model_Generator
{
    public enum BuildStrategy { Source, Assembly, Embedded };
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
                                            string connectionString)
        {
            DatabaseCreator databaseCreator;

            Console.Write("Building DBContext from source...");
            databaseCreator = new DatabaseCreator(connectionString, datasetName);
            
            Console.WriteLine("Created " + databaseCreator.DbContext.GetType().Name);
            databaseCreator.CreateMigration(datasetName + Path.GetRandomFileName());
            Console.Write("Rebuilding...");
            databaseCreator.ReloadDbContext();
            Console.WriteLine("Rebuilt!");

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
        public static async Task Main(string[] args)
        {
            var lf = new LoggerFactory();
            var l = lf.CreateLogger(typeof(DbContext));

            Console.WriteLine("This process will guide you through the creation of the data tool. Press enter to continue or ^C to exit.");
            //Console.Read();
            Console.Write("Enter dataset name: ");
            string datasetName = "CMSIF2";//Console.ReadLine().ToUpper();
            Console.Write("Enter dataset file path: ");
            string csvFilePath = "../../CMSIF.csv";//Console.ReadLine();
            var connectionString = $"Host=localhost;Port=5432;Database={datasetName};Username=postgres;SslMode=Prefer;Trust Server Certificate=true;";

            if (false)
                using (var sr = new StreamReader(csvFilePath))
                using (var csv = new CsvReader(sr, new Configuration
                {
                    Delimiter = ",",
                    Encoding = Encoding.UTF8
                }))
                {
                    csv.Read();
                    csv.ReadHeader();

                    Console.Write("Generating master entity");
                    var outputMaster = GenerateMasterSource(datasetName, csv.Context.HeaderRecord);
                    bool isValidMaster = IsCodeValid(outputMaster);
                    if (!isValidMaster)
                    {
                        throw new Exception("Failed to generate valid master file");
                    }

                    var masterFile = new FileInfo($"../Models/Contexts/{datasetName}/Master.cs");
                    masterFile.Directory.Create();
                    File.WriteAllText(masterFile.FullName, outputMaster);
                    var editedMasterValidated = false;
                    while (!editedMasterValidated)
                    {
                        Console.WriteLine($"Created {masterFile.FullName}. Press Enter when done editing.");
                        Console.ReadLine();
                        outputMaster = File.ReadAllText(masterFile.FullName);
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

                    var modelsFile = new FileInfo($"../Models/Contexts/{datasetName}/Models.cs");
                    modelsFile.Directory.Create();
                    File.WriteAllText(modelsFile.FullName, outputModels);
                    var editedModelsValidated = false;
                    while (!editedModelsValidated)
                    {
                        Console.WriteLine($"Created {modelsFile.FullName}. Press Enter when done editing.");
                        Console.ReadLine();
                        outputModels = File.ReadAllText(modelsFile.FullName);
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
                    var contextFile = new FileInfo($"../context/Contexts/{datasetName}/Context.cs");
                    contextFile.Directory.Create();
                    File.WriteAllText(contextFile.FullName, outputContext);
                }

            Console.WriteLine("Done. Confirm results and press enter to begin database initialization.");
            // Console.ReadLine();

            SetupDatabase(datasetName, csvFilePath, connectionString);
        }


        public static string ToPascalCase(string text)
        {
            return new string(text.Split(new[] { "_", " ", "-" }, StringSplitOptions.RemoveEmptyEntries).Select(s => char.ToUpperInvariant(s[0]) + s.Substring(1, s.Length - 1)).Aggregate(string.Empty, (s1, s2) => s1 + s2).Where(c => char.IsLetterOrDigit(c)).ToArray());
        }
    }

}