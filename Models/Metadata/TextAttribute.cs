using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace Models.Metadata
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple=true)]
    public class TextAttribute : Attribute
    {
        public string Name { get; set; }
        public string Culture { get; set; }
        public TextAttribute(string name, string culture)
        {
            Name = name;
            Culture = culture;
        }
    }

    [Flags]
    public enum TextAppearance
    {
        Notes,
        MeasureDescription,
        QuickStats,
        Filter
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class ShowOn: Attribute {
        public TextAppearance TextAppearance { get; set; }
        public ShowOn(TextAppearance ta) {
            this.TextAppearance = ta;
        }
    }
}