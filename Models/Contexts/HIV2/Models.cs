

// This file was written by a tool
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.Metadata;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Contexts.HIV2 {
    [Filter(0)]
    public class ColIndicatorParent {
        public int Index { get; set; }
        public int ColIndicatorParentId { get; set; }
        [Children]
        [InverseProperty("ColIndicatorParent")]
        public ICollection<ColSecondaryBreakdownName> ColSecondaryBreakdownNames { get; set; }
        [ForeignKey("DefaultColSecondaryBreakdownNameId")]
        [DefaultChild]
        public ColSecondaryBreakdownName DefaultColSecondaryBreakdownName { get; set; }
        public int? DefaultColSecondaryBreakdownNameId { get; set; }
        [Text("Indicator Parent", "en-ca")]
        [ShowOn(TextAppearance.Filter)]
        [BindToMaster("ColIndicatorParent")]
        public string ColIndicatorParentNameEn { get; set; }
        [TranslateProperty("ColIndicatorParentNameEn")]
        [Text("Indicator Parent", "fr-ca")]
        [ShowOn(TextAppearance.Filter)]
        public string ColIndicatorParentNameFr { get; set; }
    }
    [Filter(1)]
    public class ColSecondaryBreakdownName {
        public int Index { get; set; }
        public int ColSecondaryBreakdownNameId { get; set; }
        [Children]
        [InverseProperty("ColSecondaryBreakdownName")]
        public ICollection<ColLocation> ColLocations { get; set; }
        [ForeignKey("DefaultColLocationId")]
        [DefaultChild]
        public ColLocation DefaultColLocation { get; set; }
        public int? DefaultColLocationId { get; set; }
        [Text("Secondary Breakdown Name", "en-ca")]
        [ShowOn(TextAppearance.Filter)]
        [BindToMaster("ColSecondaryBreakdownName")]
        public string ColSecondaryBreakdownNameNameEn { get; set; }
        [TranslateProperty("ColSecondaryBreakdownNameNameEn")]
        [Text("Secondary Breakdown Name", "fr-ca")]
        [ShowOn(TextAppearance.Filter)]
        public string ColSecondaryBreakdownNameNameFr { get; set; }
        public int ColIndicatorParentId { get; set; }
        [Parent]
        public ColIndicatorParent ColIndicatorParent { get; set; }
        [Include]
        [BindToMaster("Include")]
        public bool Include { get; set; }
    }
    [Filter(2)]
    public class ColLocation {
        public int Index { get; set; }
        public int ColLocationId { get; set; }
        [Children]
        [InverseProperty("ColLocation")]
        public ICollection<ColValue> ColValues { get; set; }
        [ForeignKey("DefaultColValueId")]
        [DefaultChild]
        public ColValue DefaultColValue { get; set; }
        public int? DefaultColValueId { get; set; }
        [Text("Location", "en-ca")]
        [ShowOn(TextAppearance.Filter)]
        [BindToMaster("ColLocation")]
        public string ColLocationNameEn { get; set; }
        [TranslateProperty("ColLocationNameEn")]
        [Text("Location", "fr-ca")]
        [ShowOn(TextAppearance.Filter)]
        public string ColLocationNameFr { get; set; }
        public int ColSecondaryBreakdownNameId { get; set; }
        [Parent]
        public ColSecondaryBreakdownName ColSecondaryBreakdownName { get; set; }
        [ChartType]
        [BindToMaster("ChartType")]
        public ChartType ChartType { get; set; }
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
    }
    [Filter(3)]
    public class ColValue {
        public int Index { get; set; }
        public int ColValueId { get; set; }
        [Text("Value", "en-ca")]
        [ShowOn(TextAppearance.Filter)]
        [BindToMaster("ColValue")]
        public string ColValueNameEn { get; set; }
        [TranslateProperty("ColValueNameEn")]
        [Text("Value", "fr-ca")]
        [ShowOn(TextAppearance.Filter)]
        public string ColValueNameFr { get; set; }
        public int ColLocationId { get; set; }
        [Parent]
        public ColLocation ColLocation { get; set; }
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
        [Type]
        [BindToMaster("Type")]
        public int Type { get; set; }
        [AggregatorLabel]
        [Text("en-ca")]
        [BindToMaster("AggregatorLabel")]
        public string AggregatorLabelEn { get; set; }
        [AggregatorLabel]
        [TranslateProperty("AggregatorLabelEn")]
        [Text("fr-ca")]
        public string AggregatorLabelFr { get; set; }
        [AggregatorReference]
        [BindToMaster("AggregatorReference")]
        public string AggregatorReference { get; set; }
    }
    
}