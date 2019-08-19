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
        public TextAttribute(string name, string culture): this(culture)
        {
            Name = name;
        }
        public TextAttribute(string culture)
        {
            Culture = culture;
        }
        public static IDictionary<string, string> GetShowable(object row, string culture, TextAppearance ta) {
            var dict = new Dictionary<string, string>();
            var current = row;
            while (current != null) {
                var textProps = GetTextProperties(current.GetType(), culture, ta);
                foreach (var textProp in textProps) {
                    dict.Add(textProp.GetCustomAttribute<TextAttribute>().Name, (string)textProp.GetValue(current));
                }
                current = ParentAttribute.GetParentOf(current);
            }
            return dict;
        }
        public static PropertyInfo GetTextProperty(object row, string culture, TextAppearance ta) {
            return GetTextProperty(row.GetType(), culture, ta);
        }
        public static PropertyInfo GetTextProperty(Type rowType, string culture, TextAppearance ta) {
            return GetTextProperties(rowType, culture, ta).First();
        }

        public static IEnumerable<PropertyInfo> GetTextProperties(Type rowType, string culture, TextAppearance ta) {
            var properties = rowType.GetProperties().Where(p => {
                var showOn = p.GetCustomAttribute<ShowOnAttribute>()?.TextAppearance;
                var textCulture = p.GetCustomAttribute<TextAttribute>()?.Culture;
                return showOn == ta && textCulture == culture;
            });
            return properties;
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