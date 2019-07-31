using System;
using System.Collections.Generic;
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
    public class CVAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Property)]
    public class UnitLongAttribute : Attribute { }
    [AttributeUsage(AttributeTargets.Property)]
    public class UnitShortAttribute : Attribute { }
    [AttributeUsage(AttributeTargets.Property)]
    public class Included : Attribute { }
}