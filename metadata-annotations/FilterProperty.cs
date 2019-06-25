using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace metadata_annotations
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FilterProperty : Attribute
    {
        public int Level { get; set; }
        public FilterProperty(int level)
        {
            Level = level;
        }
    }
}