

// This file was written by a tool
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.Metadata;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Contexts.PASS {
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
        [TranslateProperty("ColActivityNameEn")]
        [Text("Activit&#xE9;", "fr-ca")]
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
        [TranslateProperty("ColIndicatorGroupNameEn")]
        [Text("Groupe d&#x27;indicateur", "fr-ca")]
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
        [TranslateProperty("ColLifeCourseNameEn")]
        [Text("Parcours de vie", "fr-ca")]
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
        public ICollection<ColSpecificMeasure1> ColSpecificMeasure1S { get; set; }
        [ForeignKey("DefaultColSpecificMeasure1Id")]
        [DefaultChild]
        public ColSpecificMeasure1 DefaultColSpecificMeasure1 { get; set; }
        public int? DefaultColSpecificMeasure1Id { get; set; }
        [Text("Indicators", "en-ca")]
        [ShowOn(TextAppearance.Filter)]
        [BindToMaster("ColIndicator")]
        public string ColIndicatorNameEn { get; set; }
        [TranslateProperty("ColIndicatorNameEn")]
        [Text("Indicateurs", "fr-ca")]
        [ShowOn(TextAppearance.Filter)]
        public string ColIndicatorNameFr { get; set; }
        public int ColLifeCourseId { get; set; }
        [Parent]
        public ColLifeCourse ColLifeCourse { get; set; }
    }
    [Filter(4)]
    public class ColSpecificMeasure1 {
        public int Index { get; set; }
        public int ColSpecificMeasure1Id { get; set; }
        [Children]
        [InverseProperty("ColSpecificMeasure1")]
        public ICollection<ColDataBreakdowns> ColDataBreakdowns { get; set; }
        [ForeignKey("DefaultColDataBreakdownsId")]
        [DefaultChild]
        public ColDataBreakdowns DefaultColDataBreakdowns { get; set; }
        public int? DefaultColDataBreakdownsId { get; set; }
        [Text("Measures", "en-ca")]
        [ShowOn(TextAppearance.Filter)]
        [BindToMaster("ColSpecificMeasure1")]
        public string ColSpecificMeasure1NameEn { get; set; }
        [TranslateProperty("ColSpecificMeasure1NameEn")]
        [Text("Mesures", "fr-ca")]
        [ShowOn(TextAppearance.Filter)]
        public string ColSpecificMeasure1NameFr { get; set; }
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
        public ICollection<ColDisaggregation> ColDisaggregations { get; set; }
        [ForeignKey("DefaultColDisaggregationId")]
        [DefaultChild]
        public ColDisaggregation DefaultColDisaggregation { get; set; }
        public int? DefaultColDisaggregationId { get; set; }
        [Text("Data Breakdowns", "en-ca")]
        [ShowOn(TextAppearance.Filter)]
        [BindToMaster("ColDataBreakdowns")]
        public string ColDataBreakdownsNameEn { get; set; }
        [TranslateProperty("ColDataBreakdownsNameEn")]
        [Text("R&#xE9;partition des donn&#xE9;es", "fr-ca")]
        [ShowOn(TextAppearance.Filter)]
        public string ColDataBreakdownsNameFr { get; set; }
        public int ColSpecificMeasure1Id { get; set; }
        [Parent]
        public ColSpecificMeasure1 ColSpecificMeasure1 { get; set; }
        
        [BindToMaster("ColDataSource2")]
        
        [Text("Data Source 2", "en-ca")]
        public string ColDataSource2En { get; set; }
        
        [TranslateProperty("ColDataSource2En")]
        [Text("Data Source 2", "fr-ca")]
        public string ColDataSource2Fr { get; set; }
        [BindToMaster("ColDataSource3")]
        
        [Text("Data Source 3", "en-ca")]
        public string ColDataSource3En { get; set; }
        
        [TranslateProperty("ColDataSource3En")]
        [Text("Data Source 3", "fr-ca")]
        public string ColDataSource3Fr { get; set; }
        [Title]
        [Text("en-ca")]
        [BindToMaster("Title")]
        public string TitleEn { get; set; }
        [Title]
        [TranslateProperty("TitleEn")]
        [Text("fr-ca")]
        public string TitleFr { get; set; }
        [CVRangeLower]
        [BindToMaster("CVRangeLower")]
        public double? CVRangeLower { get; set; }
        [CVRangeUpper]
        [BindToMaster("CVRangeUpper")]
        public double? CVRangeUpper { get; set; }
        [UnitLong]
        [Text("en-ca")]
        [BindToMaster("UnitLong")]
        public string UnitLongEn { get; set; }
        [UnitLong]
        [Text("fr-ca")]
        [TranslateProperty("UnitLongEn")]
        public string UnitLongFr { get; set; }
        [BindToMaster("UnitShort")]
        [UnitShort]
        [Text("en-ca")]
        public string UnitShortEn { get; set; }
        [UnitShort]
        [Text("fr-ca")]
        [TranslateProperty("UnitShortEn")]
        public string UnitShortFr { get; set; }

        [BindToMaster("ColDefinition")]
        [ShowOn(TextAppearance.MeasureDescription)]
        [Text("Definition", "en-ca")]
        public string ColDefinitionEn { get; set; }
        [ShowOn(TextAppearance.MeasureDescription)]
        [TranslateProperty("ColDefinitionEn")]
        [Text("Definition", "fr-ca")]
        public string ColDefinitionFr { get; set; }
        [BindToMaster("ColDataAvailable")]
        [ShowOn(TextAppearance.MeasureDescription)]
        [Text("Data Available", "en-ca")]
        public string ColDataAvailableEn { get; set; }
        [ShowOn(TextAppearance.MeasureDescription)]
        [TranslateProperty("ColDataAvailableEn")]
        [Text("Data Available", "fr-ca")]
        public string ColDataAvailableFr { get; set; }
        [BindToMaster("ColEstimateCalculation")]
        [ShowOn(TextAppearance.MeasureDescription)]
        [Text("Estimate Calculation", "en-ca")]
        public string ColEstimateCalculationEn { get; set; }
        [ShowOn(TextAppearance.MeasureDescription)]
        [TranslateProperty("ColEstimateCalculationEn")]
        [Text("Estimate Calculation", "fr-ca")]
        public string ColEstimateCalculationFr { get; set; }
        [BindToMaster("ColAdditionalRemarks")]
        [ShowOn(TextAppearance.MeasureDescription)]
        [Text("Additional Remarks", "en-ca")]
        public string ColAdditionalRemarksEn { get; set; }
        [ShowOn(TextAppearance.MeasureDescription)]
        [TranslateProperty("ColAdditionalRemarksEn")]
        [Text("Additional Remarks", "fr-ca")]
        public string ColAdditionalRemarksFr { get; set; }
    }
    [Filter(6)]
    public class ColDisaggregation {
        public int Index { get; set; }
        public int ColDisaggregationId { get; set; }
        [Text("Disaggregation", "en-ca")]
        [ShowOn(TextAppearance.Filter)]
        [BindToMaster("ColDisaggregation")]
        public string ColDisaggregationNameEn { get; set; }
        [TranslateProperty("ColDisaggregationNameEn")]
        [Text("Disaggregation", "fr-ca")]
        [ShowOn(TextAppearance.Filter)]
        public string ColDisaggregationNameFr { get; set; }
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
        [BindToMaster("DataLabelChart")]
        [DataLabelChart]
        [Text("en-ca")]
        public string DataLabelChartEn { get; set; }
        [DataLabelChart]
        [Text("fr-ca")]
        [TranslateProperty("DataLabelChartEn")]
        public string DataLabelChartFr { get; set; }
        [BindToMaster("DataLabelTable")]
        [DataLabelTable]
        [Text("en-ca")]
        public string DataLabelTableEn { get; set; }
        [DataLabelTable]
        [Text("fr-ca")]
        [TranslateProperty("DataLabelTableEn")]
        public string DataLabelTableFr { get; set; }
        [BindToMaster("ColDataSource1")]
        [ShowOn(TextAppearance.Notes)]
        [Text("Data Source", "en-ca")]
        public string ColDataSource1En { get; set; }
        [ShowOn(TextAppearance.Notes)]
        [TranslateProperty("ColDataSource1En")]
        [Text("Data Source", "fr-ca")]
        public string ColDataSource1Fr { get; set; }
        [BindToMaster("ColNotes")]
        [ShowOn(TextAppearance.Notes)]
        [Text("Notes", "en-ca")]
        public string ColNotesEn { get; set; }
        [ShowOn(TextAppearance.Notes)]
        [TranslateProperty("ColNotesEn")]
        [Text("Notes", "fr-ca")]
        public string ColNotesFr { get; set; }
        [Type]
        public int Type => this == ColDataBreakdowns.DefaultColDisaggregation ? 1 : 0;
    }
    
}