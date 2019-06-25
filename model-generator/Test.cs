using System;
using System.IO;
using System.Text;
using metadata_annotations;
using model_generator;
using System.Net;
namespace RoslynCompileSample
{
    public class Writer
    {
        [TextProperty("Hello", "World")]
        public string MyProperty { get; set; }
        public void Write(string message)
        {
            Console.WriteLine($"you said '{message}!'");
            Dns.Resolve("google.com");
            //Program2.DoThing();
        }
    }
}