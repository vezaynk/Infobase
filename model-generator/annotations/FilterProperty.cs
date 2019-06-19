using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace model_generator.annotations
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FilterProperty : Attribute
    {
        private static int CurrentLevel { get; set; } = 0;
        public int Level { get; set; }
        public FilterProperty()
        {
            Level = CurrentLevel++;
        }
    }
}