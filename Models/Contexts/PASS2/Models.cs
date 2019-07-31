

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
    public class Activity {
        public int Index { get; set; }
        public int ActivityId { get; set; }
        [InverseProperty("Activity")]
        public ICollection<IndicatorGroup> IndicatorGroups { get; set; }
        [ForeignKey("DefaultIndicatorGroupId")]
        public IndicatorGroup DefaultIndicatorGroup { get; set; }    
        public int? DefaultIndicatorGroupId { get; set; }    
        [Text("Activity", "en-ca")]
        [ShowOn(TextAppearance.Filter)]
        [BindToMaster("Activity")]
        public string ActivityNameEn { get; set; }
        [Text("Activity", "fr-ca")]
        [ShowOn(TextAppearance.Filter)]
        public string ActivityNameFr { get; set; }
    }
    [Filter(1)]
    public class IndicatorGroup {
        public int Index { get; set; }
        public int IndicatorGroupId { get; set; }
        [InverseProperty("IndicatorGroup")]
        public ICollection<LifeCourse> LifeCourses { get; set; }
        [ForeignKey("DefaultLifeCourseId")]
        public LifeCourse DefaultLifeCourse { get; set; }    
        public int? DefaultLifeCourseId { get; set; }    
        [Text("Indicator Group", "en-ca")]
        [ShowOn(TextAppearance.Filter)]
        [BindToMaster("IndicatorGroup")]
        public string IndicatorGroupNameEn { get; set; }
        [Text("Indicator Group", "fr-ca")]
        [ShowOn(TextAppearance.Filter)]
        public string IndicatorGroupNameFr { get; set; }
        public int ActivityId { get; set; }
        public Activity Activity { get; set; }
    }
    [Filter(2)]
    public class LifeCourse {
        public int Index { get; set; }
        public int LifeCourseId { get; set; }
        [InverseProperty("LifeCourse")]
        public ICollection<Indicator> Indicators { get; set; }
        [ForeignKey("DefaultIndicatorId")]
        public Indicator DefaultIndicator { get; set; }    
        public int? DefaultIndicatorId { get; set; }    
        [Text("Life Course", "en-ca")]
        [ShowOn(TextAppearance.Filter)]
        [BindToMaster("LifeCourse")]
        public string LifeCourseNameEn { get; set; }
        [Text("Life Course", "fr-ca")]
        [ShowOn(TextAppearance.Filter)]
        public string LifeCourseNameFr { get; set; }
        public int IndicatorGroupId { get; set; }
        public IndicatorGroup IndicatorGroup { get; set; }
    }
    [Filter(3)]
    public class Indicator {
        public int Index { get; set; }
        public int IndicatorId { get; set; }
        [InverseProperty("Indicator")]
        public ICollection<SpecificMeasure> SpecificMeasures { get; set; }
        [ForeignKey("DefaultSpecificMeasureId")]
        public SpecificMeasure DefaultSpecificMeasure { get; set; }    
        public int? DefaultSpecificMeasureId { get; set; }    
        [Text("Indicator", "en-ca")]
        [ShowOn(TextAppearance.Filter)]
        [BindToMaster("Indicator")]
        public string IndicatorNameEn { get; set; }
        [Text("Indicator", "fr-ca")]
        [ShowOn(TextAppearance.Filter)]
        public string IndicatorNameFr { get; set; }
        public int LifeCourseId { get; set; }
        public LifeCourse LifeCourse { get; set; }
    }
    [Filter(4)]
    public class SpecificMeasure {
        public int Index { get; set; }
        public int SpecificMeasureId { get; set; }
        [InverseProperty("SpecificMeasure")]
        public ICollection<DataBreakdowns> DataBreakdowns { get; set; }
        [ForeignKey("DefaultDataBreakdownsId")]
        public DataBreakdowns DefaultDataBreakdowns { get; set; }    
        public int? DefaultDataBreakdownsId { get; set; }    
        [Text("Specific Measure", "en-ca")]
        [ShowOn(TextAppearance.Filter)]
        [BindToMaster("SpecificMeasure")]
        public string SpecificMeasureNameEn { get; set; }
        [Text("Specific Measure", "fr-ca")]
        [ShowOn(TextAppearance.Filter)]
        public string SpecificMeasureNameFr { get; set; }
        public int IndicatorId { get; set; }
        public Indicator Indicator { get; set; }
    }
    [Filter(5)]
    public class DataBreakdowns {
        public int Index { get; set; }
        public int DataBreakdownsId { get; set; }
        [InverseProperty("DataBreakdowns")]
        public ICollection<Strata> Strata { get; set; }
        [ForeignKey("DefaultStrataId")]
        public Strata DefaultStrata { get; set; }    
        public int? DefaultStrataId { get; set; }    
        [Text("Data Breakdowns", "en-ca")]
        [ShowOn(TextAppearance.Filter)]
        [BindToMaster("DataBreakdowns")]
        public string DataBreakdownsNameEn { get; set; }
        [Text("Data Breakdowns", "fr-ca")]
        [ShowOn(TextAppearance.Filter)]
        public string DataBreakdownsNameFr { get; set; }
        public int SpecificMeasureId { get; set; }
        public SpecificMeasure SpecificMeasure { get; set; }
    }
    [Filter(6)]
    public class Strata {
        public int Index { get; set; }
        public int StrataId { get; set; }
        [Text("Strata", "en-ca")]
        [ShowOn(TextAppearance.Filter)]
        [BindToMaster("Strata")]
        public string StrataNameEn { get; set; }
        [Text("Strata", "fr-ca")]
        [ShowOn(TextAppearance.Filter)]
        public string StrataNameFr { get; set; }
        public int DataBreakdownsId { get; set; }
        public DataBreakdowns DataBreakdowns { get; set; }
    }
    
}
