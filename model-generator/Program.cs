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
using CSharpLoader;
using CsvHelper;

namespace model_generator
{
    class Program
    {
        public static void Main(string[] args)
        {


            var csv = new CsvReader(new StreamReader(@"./pass.csv"), new CsvHelper.Configuration.Configuration
            {
                Delimiter = ",",
                Encoding = Encoding.UTF8
            });
            csv.Read();
            csv.ReadHeader();
            Console.WriteLine(csv.Context.HeaderRecord.First());
            csv.Dispose();

            var csmemcompiler = new CSharpInMemoryCompiler("./Test.cs", "/opt/dotnet/shared/Microsoft.NETCore.App/2.2.3/");
            Type loadedWriterType = csmemcompiler.GetType("RoslynCompileSample.Writer");
            var textProperty = loadedWriterType.GetProperty("MyProperty").GetCustomAttribute<TextProperty>();
            Console.WriteLine(textProperty.Name + " " + textProperty.Culture);

            //var instance = (RoslynCompileSample.Writer)Activator.CreateInstance(loadedWriterType);

            var instance = Activator.CreateInstance(loadedWriterType);
            var meth = loadedWriterType.GetMember("Write").First() as MethodInfo;
            meth.Invoke(instance, new[] { "Hello World" });
        }

    }
}