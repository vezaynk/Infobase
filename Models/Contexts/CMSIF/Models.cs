

// This file was written by a tool
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.Metadata;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Contexts.CMSIF
{
    [Filter(0)]
    public class ColDomain
    {
        public int Index { get; set; }
        public int ColDomainId { get; set; }
        [Children]
        [InverseProperty("ColDomain")]
        public ICollection<ColIndicator> ColIndicators { get; set; }
        [ForeignKey("DefaultColIndicatorId")]
        [DefaultChild]
        public ColIndicator DefaultColIndicator { get; set; }
        public int? DefaultColIndicatorId { get; set; }
        [Text("Domain", "en-ca")]
        [ShowOn(TextAppearance.Filter)]
        [BindToMaster("ColDomain")]
        public string ColDomainNameEn { get; set; }
        [TranslateProperty("ColDomainNameEn")]
        [Text("Domain", "fr-ca")]
        [ShowOn(TextAppearance.Filter)]
        public string ColDomainNameFr { get; set; }
    }
    [Filter(1)]
    public class ColIndicator
    {
        public int Index { get; set; }
        public int ColIndicatorId { get; set; }
        [Children]
        [InverseProperty("ColIndicator")]
        public ICollection<ColMeasures> ColMeasures { get; set; }
        [ForeignKey("DefaultColMeasuresId")]
        [DefaultChild]
        public ColMeasures DefaultColMeasures { get; set; }
        public int? DefaultColMeasuresId { get; set; }
        [Text("Indicator", "en-ca")]
        [ShowOn(TextAppearance.Filter)]
        [BindToMaster("ColIndicator")]
        public string ColIndicatorNameEn { get; set; }
        [TranslateProperty("ColIndicatorNameEn")]
        [Text("Indicator", "fr-ca")]
        [ShowOn(TextAppearance.Filter)]
        public string ColIndicatorNameFr { get; set; }
        public int ColDomainId { get; set; }
        [Parent]
        public ColDomain ColDomain { get; set; }
    }
    [Filter(2)]
    public class ColMeasures
    {
        public int Index { get; set; }
        public int ColMeasuresId { get; set; }
        [Children]
        [InverseProperty("ColMeasures")]
        public ICollection<ColDataBreakdowns> ColDataBreakdowns { get; set; }
        [ForeignKey("DefaultColDataBreakdownsId")]
        [DefaultChild]
        public ColDataBreakdowns DefaultColDataBreakdowns { get; set; }
        public int? DefaultColDataBreakdownsId { get; set; }
        [Text("Measures", "en-ca")]
        [ShowOn(TextAppearance.Filter)]
        [BindToMaster("ColMeasures")]
        public string ColMeasuresNameEn { get; set; }
        [TranslateProperty("ColMeasuresNameEn")]
        [Text("Measures", "fr-ca")]
        [ShowOn(TextAppearance.Filter)]
        public string ColMeasuresNameFr { get; set; }
        public int ColIndicatorId { get; set; }
        [Parent]
        public ColIndicator ColIndicator { get; set; }
        [BindToMaster("ColSource")]
        [ShowOn(TextAppearance.MeasureDescription)]
        [Text("Source", "en-ca")]
        public string ColSourceEn { get; set; }
        [ShowOn(TextAppearance.MeasureDescription)]
        [TranslateProperty("ColSourceEn")]
        [Text("Source", "fr-ca")]
        public string ColSourceFr { get; set; }
        [Include]
        [BindToMaster("Include")]
        public bool Include { get; set; }
        [Aggregator]
        [BindToMaster("Aggregator")]
        public bool IsAggregator { get; set; }

        [BindToMaster("ColDataSource")]
        [Text("en-ca")]
        public string ColDataSourceEn { get; set; }
    }
    [Filter(3)]
    public class ColDataBreakdowns
    {
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
        [Text("Data Breakdowns", "fr-ca")]
        [ShowOn(TextAppearance.Filter)]
        public string ColDataBreakdownsNameFr { get; set; }
        public int ColMeasuresId { get; set; }
        [Parent]
        public ColMeasures ColMeasures { get; set; }
        [BindToMaster("ColNotes")]
        [ShowOn(TextAppearance.Notes)]
        [Text("Notes", "en-ca")]
        public string ColNotesEn { get; set; }
        [ShowOn(TextAppearance.Notes)]
        [TranslateProperty("ColNotesEn")]
        [Text("Notes", "fr-ca")]
        public string ColNotesFr { get; set; }
        [Title]
        [Text("en-ca")]
        public string TitleEn => $"{ColDataBreakdownsNameEn}, by {UnitLongEn}";
        [Title]
        [Text("fr-ca")]
        public string TitleFr => $"{ColDataBreakdownsNameFr}, by {UnitLongFr}";
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
    [Filter(4)]
    public class ColDisaggregation
    {
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
    }

}