using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace model_generator.annotations
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PointAverage : Attribute { }

    [AttributeUsage(AttributeTargets.Property)]
    public class PointUpper : Attribute { }

    [AttributeUsage(AttributeTargets.Property)]
    public class PointLower : Attribute { }

    [AttributeUsage(AttributeTargets.Property)]
    public class CVInterpretation : Attribute { }

    [AttributeUsage(AttributeTargets.Property)]
    public class CVRangeUpper : Attribute { }

    [AttributeUsage(AttributeTargets.Property)]
    public class CVRangeLower : Attribute { }

    [AttributeUsage(AttributeTargets.Property)]
    public class CV : Attribute { }

    [AttributeUsage(AttributeTargets.Property)]
    public class UnitLong : Attribute { }
    [AttributeUsage(AttributeTargets.Property)]
    public class UnitShort : Attribute { }

}