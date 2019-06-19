using System;
using System.IO;
using System.Text;
using model_generator;
namespace RoslynCompileSample
{
    public class Writer
    {
        public void Write(string message)
        {
            Console.WriteLine($"you said '{message}!'");
            //Program2.DoThing();
        }
    }
}