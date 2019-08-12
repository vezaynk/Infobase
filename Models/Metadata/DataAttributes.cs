using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace Models.Metadata
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PointAverageAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Property)]
    public class DataLabelTable : Attribute { }

    [AttributeUsage(AttributeTargets.Property)]
    public class DataLabelChart : Attribute { }

    [AttributeUsage(AttributeTargets.Property)]
    public class PointUpperAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Property)]
    public class PointLowerAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Property)]
    public class CVInterpretationAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Property)]
    public class CVRangeUpperAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Property)]
    public class CVRangeLowerAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Property)]
    public class CVValueAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Property)]
    public class UnitLongAttribute : Attribute { }
    [AttributeUsage(AttributeTargets.Property)]
    public class UnitShortAttribute : Attribute { }
    [AttributeUsage(AttributeTargets.Property)]
    public class IncludeAttribute : Attribute
    {
        public static bool GetIncludedState(object row)
        {
            var currentRow = row;

            while (currentRow != null)
            {
                bool isIncluded = GetIncludedStateImmediate(currentRow);
                currentRow = ParentAttribute.GetParentOf(currentRow);
                if (isIncluded == false)
                {
                    return false;
                }
            }

            return true;
        }
        public static bool GetIncludedStateImmediate(object row)
        {
            var property = row.GetType().GetProperties().FirstOrDefault(p => p.GetCustomAttribute<IncludeAttribute>() != null);
            return (bool)(property?.GetValue(row) ?? true);
        }
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class AggregatorAttribute : Attribute { }
    [AttributeUsage(AttributeTargets.Property)]
    public class ParentAttribute : Attribute
    {
        public static object GetParentOf(object child)
        {
            var property = GetParentOfProperty(child.GetType());
            return property?.GetValue(child);
        }
        public static PropertyInfo GetParentOfProperty(Type childType)
        {
            return childType.GetProperties().FirstOrDefault(p => p.GetCustomAttribute<ParentAttribute>() != null);
        }
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class DefaultChildAttribute : Attribute
    {
        public static object GetDefaultChildOf(object parent)
        {
            var property = GetDefaultChildOfProperty(parent.GetType());
            return property?.GetValue(parent);
        }
        public static PropertyInfo GetDefaultChildOfProperty(Type parentType)
        {
            return parentType.GetProperties().FirstOrDefault(p => p.GetCustomAttribute<DefaultChildAttribute>() != null);
        }
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class ChildrenAttribute : Attribute
    {
        public static IEnumerable<object> GetChildrenOf(object parent)
        {
            var property = GetChildrenOfProperty(parent.GetType());
            try
            {

                return Enumerable.Cast<object>((IEnumerable)property?.GetValue(parent));
            }
            catch (System.Exception)
            {

                return Enumerable.Empty<object>();
            }
        }
        public static PropertyInfo GetChildrenOfProperty(Type parentType)
        {
            return parentType.GetProperties().FirstOrDefault(p => p.GetCustomAttribute<ChildrenAttribute>() != null);
        }
    }
}