using System;
using System.IO;
using System.Text;
namespace RoslynCompileSample
{
    public class Writer
    {
        public void Write(string message)
        {
            //Console.WriteLine($"you said '{message}!'");
            File.WriteAllBytes("./Hi.txt", Encoding.ASCII.GetBytes(message));
        }
    }
}