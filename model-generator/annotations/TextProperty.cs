using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace model_generator.annotations
{
    [AttributeUsage(AttributeTargets.Property)]
    public class TextProperty : Attribute
    {
        public string Name { get; set; }
        public string Culture { get; set; }
        public TextProperty(string name, string culture)
        {
            Name = name;
            Culture = culture;
        }
    }
}