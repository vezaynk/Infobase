using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace Models.Metadata
{
    public class MetadataProperty
    {
        public string Name { get; set; }
        public object Value { get; set; }
    }
    /// <summary>Helpful methods to work with metadata attributes</summary>
    public static class Metadata
    {
        /// <summary>Retrieve all parent nodes, starting with the given node</summary>
        public static IEnumerable<object> GetAllParentNodes(object node)
        {
            var current = node;
            while (current != null)
            {
                yield return current;
                current = GetParentOf(current);
            };
        }
        /// <summary>Retrieve all default child nodes, starting with the given node</summary>
        public static IEnumerable<object> GetAllDefaultChildrenNodes(object node)
        {
            var current = node;
            while (current != null)
            {
                yield return current;
                current = GetDefaultChildOf(current);
            };
        }

        /// <summary>Retrieve all properties which have an attribute T on a given Type</summary>
        public static IEnumerable<PropertyInfo> FindPropertiesOnType<T>(Type type) where T : Attribute =>
            type.GetProperties().Where(p => p.GetCustomAttribute<T>() != null);

        /// <summary>Retrieves the first property which has an attribute T on a given Type</summary>
        public static PropertyInfo FindPropertyOnType<T>(Type type) where T : Attribute =>
            FindPropertiesOnType<T>(type).FirstOrDefault();

        /// <summary>Get all key-value pairings of designated by TextAttribute of a node</summary>
        public static IEnumerable<MetadataProperty> FindTextPropertiesOnNode<T>(object node, string languageCode = null, TextAppearance textAppearance = TextAppearance.None) where T : Attribute =>
            FindPropertiesOnType<T>(node.GetType())
                .Where(p => languageCode == null || p.GetCustomAttribute<TextAttribute>()?.Culture == languageCode)
                .Where(p => textAppearance == TextAppearance.None ^ p.GetCustomAttribute<ShowOnAttribute>()?.TextAppearance.HasFlag(textAppearance) == true)
                .Select(p => new MetadataProperty
                {
                    Name = p.GetCustomAttribute<TextAttribute>()?.Name,
                    Value = p.GetValue(node)
                });

        public static IEnumerable<MetadataProperty> FindTextPropertiesOnNode(object node, string languageCode = null, TextAppearance textAppearance = TextAppearance.None) =>
            FindTextPropertiesOnNode<TextAttribute>(node, languageCode, textAppearance);

        public static IEnumerable<MetadataProperty> FindTextPropertiesOnTree<T>(object node, string languageCode = null, TextAppearance textAppearance = TextAppearance.None) where T : Attribute =>
            GetAllParentNodes(node).Concat(GetAllDefaultChildrenNodes(node)).Distinct()
                .SelectMany(n => FindTextPropertiesOnNode<T>(n, languageCode, textAppearance));

        public static IEnumerable<MetadataProperty> FindTextPropertiesOnTree(object node, string languageCode = null, TextAppearance textAppearance = TextAppearance.None) =>
            FindTextPropertiesOnTree<TextAttribute>(node, languageCode, textAppearance);

        public static object GetParentOf(object child)
        {
            var property = FindPropertyOnType<ParentAttribute>(child.GetType());
            if (property != null)
                return property.GetValue(child);

            return null;
        }
        public static object GetDefaultChildOf(object parent)
        {
            var property = FindPropertyOnType<DefaultChildAttribute>(parent.GetType());
            if (property != null)
                return property.GetValue(parent);

            return null;
        }
        public static bool GetIncludedState(object row) => (bool?)FindTextPropertiesOnTree<IncludeAttribute>(row).FirstOrDefault()?.Value != false;

    }
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class TextAttribute : Attribute
    {
        public string Name { get; set; }
        public string Culture { get; set; }
        public TextAttribute(string name, string culture) : this(culture)
        {
            Name = name;
        }
        public TextAttribute(string culture)
        {
            Culture = culture;
        }
    }

    [Flags]
    public enum TextAppearance
    {
        None = 0,
        Notes = 1,
        MeasureDescription = 2,
        Filter = 4
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class ShowOnAttribute : Attribute
    {
        public TextAppearance TextAppearance { get; set; } = TextAppearance.None;
        public ShowOnAttribute(TextAppearance ta)
        {
            this.TextAppearance = ta;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class TranslatePropertyAttribute : Attribute
    {
        public string Property { get; set; }
        public TranslatePropertyAttribute(string property)
        {
            this.Property = property;
        }
    }
}