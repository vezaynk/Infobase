// This file was written by a tool
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.Metadata;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Contexts.CMSIF {
    [Filter(0)]
    public class ColDomain {
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
    public class ColIndicator {
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
    public class ColMeasures {
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
        [Include]
        [BindToMaster("Include")]
        public bool Include { get; set; }
        [Aggregator]
        [BindToMaster("Aggregator")]
        public bool IsAggregator { get; set; }
    }
    [Filter(3)]
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
        [Text("Data Breakdowns", "fr-ca")]
        [ShowOn(TextAppearance.Filter)]
        public string ColDataBreakdownsNameFr { get; set; }
        public int ColMeasuresId { get; set; }
        [Parent]
        public ColMeasures ColMeasures { get; set; }
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
    [Filter(4)]
    public class ColDisaggregation {
        public int Index { get; set; }
        public int ColDisaggregationId { get; set; }
        [Text("Disaggregation", "en-ca")]
        [ShowOn(TextAppearance.Filter)]
        [DataLabelChart]
        [DataLabelTable]
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
    }
    
}