
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
using model_generator.annotations;
namespace model_generator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Program2.DoThing();

        }

    }

    public class Program2
    {
        static Action<string> Write = Console.WriteLine;

        public static void DoThing()
        {
            Write("Let's compile!");

            string codeToCompile = @"
            using System;
            using System.IO;
            using System.Text;
            using model_generator;
            using model_generator.annotations;
            namespace RoslynCompileSample
            {
                [TextProperty(""Hello"",""World"")]
                public class Writer
                {
                    public void Write(string message)
                    {
                        Console.WriteLine($""you said '{message}!'"");
                        //File.WriteAllBytes(""./Hi.txt"", Encoding.ASCII.GetBytes(message));
                        //Program2.DoThing();
                    }
                }
            }";
            File.WriteAllBytes("./Hi.txt", Encoding.ASCII.GetBytes(""));
            Write("Parsing the code into the SyntaxTree");
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(codeToCompile);

            string assemblyName = Path.GetRandomFileName();
            var references = Directory
                                .GetFileSystemEntries("/opt/dotnet/shared/Microsoft.NETCore.App/2.2.3/", "*.dll")
                                .Append(Assembly.GetExecutingAssembly().Location)
                                .Select(path => MetadataReference.CreateFromFile(path));


            Write(typeof(object).Assembly.Location);

            Write("Compiling ...");
            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName,
                syntaxTrees: new[] { syntaxTree },
                references: references,
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            using (var ms = new MemoryStream())
            {
                EmitResult result = compilation.Emit(ms);

                if (!result.Success)
                {
                    Write("Compilation failed!");
                    IEnumerable<Diagnostic> failures = result.Diagnostics.Where(diagnostic =>
                        diagnostic.IsWarningAsError ||
                        diagnostic.Severity == DiagnosticSeverity.Error);

                    foreach (Diagnostic diagnostic in failures)
                    {
                        Console.Error.WriteLine("\t{0}: {1}", diagnostic.Id, diagnostic.GetMessage());
                    }
                }
                else
                {
                    Write("Compilation successful! Now instantiating and executing the code ...");
                    ms.Seek(0, SeekOrigin.Begin);

                    Assembly assembly = AssemblyLoadContext.Default.LoadFromStream(ms);
                    var type = assembly.GetType("RoslynCompileSample.Writer");
                    var instance = assembly.CreateInstance("RoslynCompileSample.Writer");
                    var meth = type.GetMember("Write").First() as MethodInfo;
                    type.GetCustomAttribute<TextProperty>();
                    meth.Invoke(instance, new[] { "Hello World" });
                }
            }


        }
    }
}