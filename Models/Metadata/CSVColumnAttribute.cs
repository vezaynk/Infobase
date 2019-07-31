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

    [AttributeUsage(AttributeTargets.Property)]
    public class BindToMasterAttribute: Attribute {
        public string MasterPropertyName { get; set; }
        public BindToMasterAttribute(string masterProperty)
        {
            MasterPropertyName = masterProperty;
        }
    }
}