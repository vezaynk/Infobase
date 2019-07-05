using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace metadata_annotations
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FilterPropertyAttribute : Attribute
    {
        public int Level { get; set; }
        public FilterPropertyAttribute(int level)
        {
            Level = level;
        }
    }
}