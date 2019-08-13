// This file was written by a tool
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.Metadata;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Contexts.PASS2 {
    [Filter(0)]
    public class ColActivity {
        public int Index { get; set; }
        public int ColActivityId { get; set; }
        [Children]
        [InverseProperty("ColActivity")]
        public ICollection<ColIndicatorGroup> ColIndicatorGroups { get; set; }
        [ForeignKey("DefaultColIndicatorGroupId")]
        [DefaultChild]
        public ColIndicatorGroup DefaultColIndicatorGroup { get; set; }
        public int? DefaultColIndicatorGroupId { get; set; }
        [Text("Activity", "en-ca")]
        [ShowOn(TextAppearance.Filter)]
        [BindToMaster("ColActivity")]
        public string ColActivityNameEn { get; set; }
        [Text("Activity", "fr-ca")]
        [ShowOn(TextAppearance.Filter)]
        public string ColActivityNameFr { get; set; }
    }
    [Filter(1)]
    public class ColIndicatorGroup {
        public int Index { get; set; }
        public int ColIndicatorGroupId { get; set; }
        [Children]
        [InverseProperty("ColIndicatorGroup")]
        public ICollection<ColLifeCourse> ColLifeCourses { get; set; }
        [ForeignKey("DefaultColLifeCourseId")]
        [DefaultChild]
        public ColLifeCourse DefaultColLifeCourse { get; set; }
        public int? DefaultColLifeCourseId { get; set; }
        [Text("Indicator Group", "en-ca")]
        [ShowOn(TextAppearance.Filter)]
        [BindToMaster("ColIndicatorGroup")]
        public string ColIndicatorGroupNameEn { get; set; }
        [Text("Indicator Group", "fr-ca")]
        [ShowOn(TextAppearance.Filter)]
        public string ColIndicatorGroupNameFr { get; set; }
        public int ColActivityId { get; set; }
        [Parent]
        public ColActivity ColActivity { get; set; }
    }
    [Filter(2)]
    public class ColLifeCourse {
        public int Index { get; set; }
        public int ColLifeCourseId { get; set; }
        [Children]
        [InverseProperty("ColLifeCourse")]
        public ICollection<ColIndicator> ColIndicators { get; set; }
        [ForeignKey("DefaultColIndicatorId")]
        [DefaultChild]
        public ColIndicator DefaultColIndicator { get; set; }
        public int? DefaultColIndicatorId { get; set; }
        [Text("Life Course", "en-ca")]
        [ShowOn(TextAppearance.Filter)]
        [BindToMaster("ColLifeCourse")]
        public string ColLifeCourseNameEn { get; set; }
        [Text("Life Course", "fr-ca")]
        [ShowOn(TextAppearance.Filter)]
        public string ColLifeCourseNameFr { get; set; }
        public int ColIndicatorGroupId { get; set; }
        [Parent]
        public ColIndicatorGroup ColIndicatorGroup { get; set; }
    }
    [Filter(3)]
    public class ColIndicator {
        public int Index { get; set; }
        public int ColIndicatorId { get; set; }
        [Children]
        [InverseProperty("ColIndicator")]
        public ICollection<ColSpecificMeasure> ColSpecificMeasures { get; set; }
        [ForeignKey("DefaultColSpecificMeasureId")]
        [DefaultChild]
        public ColSpecificMeasure DefaultColSpecificMeasure { get; set; }
        public int? DefaultColSpecificMeasureId { get; set; }
        [Text("Indicator", "en-ca")]
        [ShowOn(TextAppearance.Filter)]
        [BindToMaster("ColIndicator")]
        public string ColIndicatorNameEn { get; set; }
        [Text("Indicator", "fr-ca")]
        [ShowOn(TextAppearance.Filter)]
        public string ColIndicatorNameFr { get; set; }
        public int ColLifeCourseId { get; set; }
        [Parent]
        public ColLifeCourse ColLifeCourse { get; set; }
    }
    [Filter(4)]
    public class ColSpecificMeasure {
        public int Index { get; set; }
        public int ColSpecificMeasureId { get; set; }
        [Children]
        [InverseProperty("ColSpecificMeasure")]
        public ICollection<ColDataBreakdowns> ColDataBreakdowns { get; set; }
        [ForeignKey("DefaultColDataBreakdownsId")]
        [DefaultChild]
        public ColDataBreakdowns DefaultColDataBreakdowns { get; set; }
        public int? DefaultColDataBreakdownsId { get; set; }
        [Text("Specific Measure", "en-ca")]
        [ShowOn(TextAppearance.Filter)]
        [BindToMaster("ColSpecificMeasure")]
        public string ColSpecificMeasureNameEn { get; set; }
        [Text("Specific Measure", "fr-ca")]
        [ShowOn(TextAppearance.Filter)]
        public string ColSpecificMeasureNameFr { get; set; }
        public int ColIndicatorId { get; set; }
        [Parent]
        public ColIndicator ColIndicator { get; set; }
        [Include]
        [BindToMaster("Include")]
        public bool Include { get; set; }
        [Aggregator]
        [BindToMaster("Aggregator")]
        public bool IsAggregator { get; set; }
    }
    [Filter(5)]
    public class ColDataBreakdowns {
        public int Index { get; set; }
        public int ColDataBreakdownsId { get; set; }
        [Children]
        [InverseProperty("ColDataBreakdowns")]
        public ICollection<ColStrata> ColStrata { get; set; }
        [ForeignKey("DefaultColStrataId")]
        [DefaultChild]
        public ColStrata DefaultColStrata { get; set; }
        public int? DefaultColStrataId { get; set; }
        [Text("Data Breakdowns", "en-ca")]
        [ShowOn(TextAppearance.Filter)]
        [BindToMaster("ColDataBreakdowns")]
        public string ColDataBreakdownsNameEn { get; set; }
        [Text("Data Breakdowns", "fr-ca")]
        [ShowOn(TextAppearance.Filter)]
        public string ColDataBreakdownsNameFr { get; set; }
        public int ColSpecificMeasureId { get; set; }
        [Parent]
        public ColSpecificMeasure ColSpecificMeasure { get; set; }
        [CVRangeLower]
        [BindToMaster("CVRangeLower")]
        public double? CVRangeLower { get; set; }
        [CVRangeUpper]
        [BindToMaster("CVRangeUpper")]
        public double? CVRangeUpper { get; set; }
        [UnitLong]
        [BindToMaster("UnitLong")]
        public string UnitLong { get; set; }
        [UnitShort]
        [BindToMaster("UnitShort")]
        public string UnitShort { get; set; }
    }
    [Filter(6)]
    public class ColStrata {
        public int Index { get; set; }
        public int ColStrataId { get; set; }
        [Text("Strata", "en-ca")]
        [ShowOn(TextAppearance.Filter)]
        [DataLabelChart]
        [DataLabelTable]
        [BindToMaster("ColStrata")]
        public string ColStrataNameEn { get; set; }
        [Text("Strata", "fr-ca")]
        [ShowOn(TextAppearance.Filter)]
        public string ColStrataNameFr { get; set; }
        public int ColDataBreakdownsId { get; set; }
        [Parent]
        public ColDataBreakdowns ColDataBreakdowns { get; set; }
        [PointAverage]
        [BindToMaster("PointAverage")]
        public double? ValueAverage { get; set; }
        [PointUpper]
        [BindToMaster("PointUpper")]
        public double? ValueUpper { get; set; }
        [PointLower]
        [BindToMaster("PointLower")]
        public double? ValueLower { get; set; }
        [CVInterpretation]
        [BindToMaster("CVInterpretation")]
        public int CVInterpretation { get; set; }
        [CVValue]
        [BindToMaster("CVValue")]
        public double? CVValue { get; set; }
    }
    
}