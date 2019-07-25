using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace Models.Metadata
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class, AllowMultiple=true)]
    public class TextAttribute : Attribute
    {
        public string Name { get; set; }
        public string Culture { get; set; }
        public TextAppearance TextAppearance { get; set; }
        public TextAttribute(string name, string culture, TextAppearance textAppearance): this(name, culture)
        {
            TextAppearance = textAppearance;
        }
        public TextAttribute(string name, string culture)
        {
            Name = name;
            Culture = culture;
            TextAppearance = TextAppearance.None;
        }
    }

    [Flags]
    public enum TextAppearance
    {
        None,
        Notes,
        MeasureDescription,
        QuickStats
    }
}