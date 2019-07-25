using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace Models.Metadata
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DatabaseAttribute : Attribute
    {
        public DatabaseAttribute(string databaseName)
        {
            this.DatabaseName = databaseName;

        }
        public string DatabaseName { get; set; }
    }
}