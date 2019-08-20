

// This file was written by a tool
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.Metadata;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Contexts.CMSIF2
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
        [Text("Measures", "fr-ca")]
        [ShowOn(TextAppearance.Filter)]
        public string ColMeasuresNameFr { get; set; }
        public int ColIndicatorId { get; set; }
        [Parent]
        public ColIndicator ColIndicator { get; set; }
        [BindToMaster("ColPopulation")]
        [ShowOn(TextAppearance.MeasureDescription)]
        [Text("Population", "en-ca")]
        public string ColPopulationEn { get; set; }
        [ShowOn(TextAppearance.MeasureDescription)]
        [Text("Population", "fr-ca")]
        public string ColPopulationFr { get; set; }
        [BindToMaster("ColMeasure")]
        [ShowOn(TextAppearance.MeasureDescription)]
        [Text("Measure", "en-ca")]
        public string ColMeasureEn { get; set; }
        [ShowOn(TextAppearance.MeasureDescription)]
        [Text("Measure", "fr-ca")]
        public string ColMeasureFr { get; set; }
        [BindToMaster("ColDefinition")]
        [ShowOn(TextAppearance.MeasureDescription)]
        [Text("Definition", "en-ca")]
        public string ColDefinitionEn { get; set; }
        [ShowOn(TextAppearance.MeasureDescription)]
        [Text("Definition", "fr-ca")]
        public string ColDefinitionFr { get; set; }
        [BindToMaster("ColDataSource")]
        [ShowOn(TextAppearance.MeasureDescription)]
        [Text("Data Source", "en-ca")]
        public string ColDataSourceEn { get; set; }
        [ShowOn(TextAppearance.MeasureDescription)]
        [Text("Data Source", "fr-ca")]
        public string ColDataSourceFr { get; set; }
        [BindToMaster("ColDataAvailable")]
        [ShowOn(TextAppearance.MeasureDescription)]
        [Text("Data Available", "en-ca")]
        public string ColDataAvailableEn { get; set; }
        [ShowOn(TextAppearance.MeasureDescription)]
        [Text("Data Available", "fr-ca")]
        public string ColDataAvailableFr { get; set; }
        [BindToMaster("ColEstimateCalculation")]
        [ShowOn(TextAppearance.MeasureDescription)]
        [Text("Estimate Calculation", "en-ca")]
        public string ColEstimateCalculationEn { get; set; }
        [ShowOn(TextAppearance.MeasureDescription)]
        [Text("Estimate Calculation", "fr-ca")]
        public string ColEstimateCalculationFr { get; set; }
        [Include]
        [BindToMaster("Include")]
        public bool Include { get; set; }
        [Aggregator]
        [BindToMaster("Aggregator")]
        public bool IsAggregator { get; set; }
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
        [Text("Data Breakdowns", "fr-ca")]
        [ShowOn(TextAppearance.Filter)]
        public string ColDataBreakdownsNameFr { get; set; }
        public int ColMeasuresId { get; set; }
        [Parent]
        public ColMeasures ColMeasures { get; set; }
        [BindToMaster("ColSource")]
        [ShowOn(TextAppearance.Notes)]
        [Text("Source", "en-ca")]
        public string ColSourceEn { get; set; }
        [ShowOn(TextAppearance.Notes)]
        [Text("Source", "fr-ca")]
        public string ColSourceFr { get; set; }
        [BindToMaster("ColNotes")]
        [ShowOn(TextAppearance.Notes)]
        [Text("Notes", "en-ca")]
        public string ColNotesEn { get; set; }
        [ShowOn(TextAppearance.Notes)]
        [Text("Notes", "fr-ca")]
        public string ColNotesFr { get; set; }
        [BindToMaster("ColAdditionalRemarks")]
        [ShowOn(TextAppearance.Notes)]
        [Text("Additional Remarks", "en-ca")]
        public string ColAdditionalRemarksEn { get; set; }
        [ShowOn(TextAppearance.Notes)]
        [Text("Additional Remarks", "fr-ca")]
        public string ColAdditionalRemarksFr { get; set; }
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
        [Text("en-fr")]
        public string UnitLongFr { get; set; }
        [BindToMaster("UnitShort")]
        [UnitShort]
        [Text("en-ca")]
        public string UnitShortEn { get; set; }
        [UnitShort]
        [Text("fr-ca")]
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
        public string DataLabelChartFr { get; set; }
        [BindToMaster("DataLabelTable")]
        [DataLabelTable]
        [Text("en-ca")]
        public string DataLabelTableEn { get; set; }
        [DataLabelTable]
        [Text("fr-ca")]
        public string DataLabelTableFr { get; set; }
    }

}