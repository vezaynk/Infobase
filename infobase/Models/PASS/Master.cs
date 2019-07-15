using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using metadata_annotations;

namespace Infobase.Models.PASS
{
        public class Master
        {
            [Key]
            public int Index { get; set; }
            [Text("Activity", "en-ca")]
            [Text("Activity", "fr-ca")]
            [CSVColumn("Activity")]
            public string Activity { get; set; }
            [Text("Indicator Group", "en-ca")]
            [Text("Indicator Group", "fr-ca")]
            [CSVColumn("Indicator Group")]
            public string IndicatorGroup { get; set; }
            [Text("Life Course", "en-ca")]
            [Text("Life Course", "fr-ca")]
            [CSVColumn("Life Course")]
            public string LifeCourse { get; set; }
            [Text("Indicator", "en-ca")]
            [Text("Indicator", "fr-ca")]
            [CSVColumn("Indicator")]
            public string Indicator { get; set; }
            [Text("Specific Measure", "en-ca")]
            [Text("Specific Measure", "fr-ca")]
            [CSVColumn("Specific Measure")]
            public string SpecificMeasure { get; set; }
            [Text("Data Breakdowns", "en-ca")]
            [Text("Data Breakdowns", "fr-ca")]
            [CSVColumn("Data Breakdowns")]
            public string DataBreakdowns { get; set; }
            [Text("Strata", "en-ca")]
            [Text("Strata", "fr-ca")]
            [CSVColumn("Strata")]
            public string Strata { get; set; }
            [Text("Population 1", "en-ca")]
            [Text("Population 1", "fr-ca")]
            [CSVColumn("Population 1")]
            public string Population1 { get; set; }
            [Text("Unit Label Long", "en-ca")]
            [Text("Unit Label Long", "fr-ca")]
            [CSVColumn("Unit Label Long")]
            public string UnitLabelLong { get; set; }
            [Text("Data Source 1", "en-ca")]
            [Text("Data Source 1", "fr-ca")]
            [CSVColumn("Data Source 1")]
            public string DataSource1 { get; set; }
            [Text("Notes", "en-ca")]
            [Text("Notes", "fr-ca")]
            [CSVColumn("Notes")]
            public string Notes { get; set; }
            [Text("PT Table Label", "en-ca")]
            [Text("PT Table Label", "fr-ca")]
            [CSVColumn("PT Table Label")]
            public string PTTableLabel { get; set; }
            [Text("Unit Label 2", "en-ca")]
            [Text("Unit Label 2", "fr-ca")]
            [CSVColumn("Unit Label 2")]
            public string UnitLabel2 { get; set; }
            [Text("Data Source 2", "en-ca")]
            [Text("Data Source 2", "fr-ca")]
            [CSVColumn("Data Source 2")]
            public string DataSource2 { get; set; }
            [Text("Specific Measure 2", "en-ca")]
            [Text("Specific Measure 2", "fr-ca")]
            [CSVColumn("Specific Measure 2")]
            public string SpecificMeasure2 { get; set; }
            [Text("Defintion", "en-ca")]
            [Text("Defintion", "fr-ca")]
            [CSVColumn("Defintion")]
            public string Defintion { get; set; }
            [Text("Data Source 3", "en-ca")]
            [Text("Data Source 3", "fr-ca")]
            [CSVColumn("Data Source 3")]
            public string DataSource3 { get; set; }
            [Text("Data Available", "en-ca")]
            [Text("Data Available", "fr-ca")]
            [CSVColumn("Data Available")]
            public string DataAvailable { get; set; }
            [Text("Population 2", "en-ca")]
            [Text("Population 2", "fr-ca")]
            [CSVColumn("Population 2")]
            public string Population2 { get; set; }
            [Text("Estimate Calculation", "en-ca")]
            [Text("Estimate Calculation", "fr-ca")]
            [CSVColumn("Estimate Calculation")]
            public string EstimateCalculation { get; set; }
            [Text("Additional Remarks", "en-ca")]
            [Text("Additional Remarks", "fr-ca")]
            [CSVColumn("Additional Remarks")]
            public string AdditionalRemarks { get; set; }

            [CSVColumn("Is Included")]
            public string IsIncluded { get; set; }
            [CSVColumn("Is Aggregator")]
            public string IsAggregator { get; set; }
            [CSVColumn("CV")]
            public string CV { get; set; }
            [CSVColumn("Data")]
            public string Data { get; set; }
            [CSVColumn("CI_low_95")]
            public string CILow95 { get; set; }
            [CSVColumn("CI_Upper_95")]
            public string CIUpper95 { get; set; }
            [CSVColumn("CV_Interpretation")]
            public string CVInterpretation { get; set; }
            [CSVColumn("CV Range Lower")]
            public string CVRangeLower { get; set; }
            [CSVColumn("CV Range Upper")]
            public string CVRangeUpper { get; set; }
            [CSVColumn("Feature Data")]
            public string FeatureData { get; set; }
        }
    }
