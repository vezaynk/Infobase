using System;
using System.IO;
using System.Text;
using model_generator.annotations;
using model_generator;
namespace RoslynCompileSample
{
    public class Writer
    {
        [TextProperty("Hello", "World")]
        public string MyProperty { get; set; }
        public void Write(string message)
        {
            Console.WriteLine($"you said '{message}!'");
            //Program2.DoThing();
        }
    }
}