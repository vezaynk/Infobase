using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace metadata_annotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple=true)]
    public class TextPropertyAttribute : Attribute
    {
        public string Name { get; set; }
        public string Culture { get; set; }
        public TextPropertyAppearance TextPropertyAppearance { get; set; }
        public TextPropertyAttribute(string name, string culture, TextPropertyAppearance textPropertyAppearance): this(name, culture)
        {
            TextPropertyAppearance = textPropertyAppearance;
        }
        public TextPropertyAttribute(string name, string culture)
        {
            Name = name;
            Culture = culture;
            TextPropertyAppearance = TextPropertyAppearance.None;
        }
    }

    [Flags]
    public enum TextPropertyAppearance
    {
        None,
        Notes,
        MeasureDescription,
        QuickStats
    }
}