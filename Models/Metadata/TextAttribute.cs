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
        public static PropertyInfo GetTextProperty(object row, string culture, TextAppearance ta) {
            return GetTextProperty(row.GetType(), culture, ta);
        }
        public static PropertyInfo GetTextProperty(Type rowType, string culture, TextAppearance ta) {
            var property = rowType.GetProperties().First(p => {
                var showOn = p.GetCustomAttribute<ShowOnAttribute>()?.TextAppearance;
                var textCulture = p.GetCustomAttribute<TextAttribute>()?.Culture;
                return showOn == ta && textCulture == culture;
            });
            return property;
        }
    }

    [Flags]
    public enum TextAppearance
    {
        None,
        Notes,
        MeasureDescription,
        Filter
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class ShowOnAttribute: Attribute {
        public TextAppearance TextAppearance { get; set; }
        public ShowOnAttribute(TextAppearance ta) {
            this.TextAppearance = ta;
        }
    }
}