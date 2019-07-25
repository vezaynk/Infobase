using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace Models.Metadata
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CSVColumnAttribute : Attribute
    {
        public string CSVColumnName { get; set; }
        public CSVColumnAttribute(string csvColumnName)
        {
            CSVColumnName = csvColumnName;
        }
    }
}