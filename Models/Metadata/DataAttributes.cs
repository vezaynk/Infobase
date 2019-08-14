using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace Models.Metadata
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PointAverageAttribute : Attribute {
        public static double? GetPointAverage(object row) {
            return (double?)row.GetType().GetProperties().First(p => p.GetCustomAttribute<PointAverageAttribute>() != null).GetValue(row);
        } 
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class DataLabelTableAttribute : Attribute {
        public static string GetDataLabelTable(object row, string culture) {
            return (string)row.GetType().GetProperties().FirstOrDefault(p => p.GetCustomAttribute<TextAttribute>()?.Culture == culture && p.GetCustomAttribute<DataLabelTableAttribute>() != null).GetValue(row);
        } 
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class DataLabelChartAttribute : Attribute {
        public static string GetDataLabelChart(object row, string culture) {
            return (string)row.GetType().GetProperties().FirstOrDefault(p => p.GetCustomAttribute<TextAttribute>()?.Culture == culture && p.GetCustomAttribute<DataLabelChartAttribute>() != null).GetValue(row);
        } 
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class PointUpperAttribute : Attribute {
        public static double? GetPointUpper(object row) {
            return (double?)row.GetType().GetProperties().First(p => p.GetCustomAttribute<PointUpperAttribute>() != null).GetValue(row);
        } 
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class PointLowerAttribute : Attribute {
        public static double? GetPointLower(object row) {
            return (double?)row.GetType().GetProperties().First(p => p.GetCustomAttribute<PointLowerAttribute>() != null).GetValue(row);
        } 
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class CVInterpretationAttribute : Attribute {
        public static int GetCVInterpretation(object row) {
            return (int)row.GetType().GetProperties().First(p => p.GetCustomAttribute<CVInterpretationAttribute>() != null).GetValue(row);
        } 
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class CVRangeUpperAttribute : Attribute {
        public static double? GetCVRangeUpper(object row) {
            return (double?)row.GetType().GetProperties().First(p => p.GetCustomAttribute<CVRangeUpperAttribute>() != null).GetValue(row);
        } 
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class CVRangeLowerAttribute : Attribute {
        public static double? GetCVRangeLower(object row) {
            return (double?)row.GetType().GetProperties().First(p => p.GetCustomAttribute<CVRangeLowerAttribute>() != null).GetValue(row);
        } 
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class CVValueAttribute : Attribute {
        public static double? GetCVValue(object row) {
            return (double?)row.GetType().GetProperties().First(p => p.GetCustomAttribute<CVValueAttribute>() != null).GetValue(row);
        } 
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class UnitLongAttribute : Attribute {
        public static string GetUnitLong(object row, string culture) {
            return (string)row.GetType().GetProperties().FirstOrDefault(p => p.GetCustomAttribute<TextAttribute>()?.Culture == culture && p.GetCustomAttribute<UnitLongAttribute>() != null).GetValue(row);
        } 
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class UnitShortAttribute : Attribute {
        public static string GetUnitShort(object row, string culture) {
            return (string)row.GetType().GetProperties().FirstOrDefault(p => p.GetCustomAttribute<TextAttribute>()?.Culture == culture && p.GetCustomAttribute<UnitShortAttribute>() != null).GetValue(row);
        } 
    }
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
            return true;
            return (bool)(property.GetValue(row) ?? true);
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
            if (property != null)
                return property.GetValue(child);
            
            return null;
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
            if (property != null)
                return property.GetValue(parent);
            
            return null;
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

                return Enumerable.Cast<object>((IEnumerable)property.GetValue(parent));
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