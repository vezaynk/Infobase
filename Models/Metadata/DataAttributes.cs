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
    public class DataLabelTableAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Property)]
    public class DataLabelChartAttribute : Attribute { }

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
    public class IncludeAttribute : Attribute { }
    [AttributeUsage(AttributeTargets.Property)]
    public class ChartTypeAttribute : Attribute { }
    [AttributeUsage(AttributeTargets.Property)]
    public class ParentAttribute : Attribute {}
    [AttributeUsage(AttributeTargets.Property)]
    public class DefaultChildAttribute : Attribute {}
    [AttributeUsage(AttributeTargets.Property)]
    public class ChildrenAttribute : Attribute {}
    [AttributeUsage(AttributeTargets.Property)]
    public class TypeAttribute : Attribute {}
    public class TitleAttribute : Attribute {}
    public class AggregatorLabelAttribute : Attribute {}
    public class AggregatorReferenceAttribute: Attribute {}
}