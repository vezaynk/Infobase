// This file was written by a tool
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.Metadata;
using System.ComponentModel.DataAnnotations;

namespace Models.Contexts.CMSIF {
    public class Master {
        /**
            It is important to not forget to use the following annotations also (Cut and paste as necessary).`
            If the needed data column is derivable, include it in the generate data models later.

            ** Data Properties **
                [CVRangeUpper]
                [CVRangeLower]
                [CVValue]
                [CVInterpretation] (May be derivable based on where the CV falls relative to CV Ranges)
                
                [PointAverage]
                [PointUpper]
                [PointLower]
                
                [UnitShort]
                [UnitLong] (Can be figured out by looking at UnitShort)

                [DataLabelTable]
                [DataLabelChart]

                [Title]                

            ** Filters **
                [Filter(0)] <- Top level
                [Filter(1)]
                [Filter(...)]
                [Filter(5)] <- Third to last level: Contains information common to all breakdowns within it (i.e. source, population, etc)
                [Filter(6)] <- Second to last level: Data breakdown (by sex, by age, etc)
                [Filter(7)] <- Last level: Point (actual data)

            ** Display properties **
                [Text("Visible Text", "en-ca/fr-ca")] <- This text will be show alongside wherever the contents appear. (Directed by [ShowOn(...)])
                [ShowOn(TextAppearance.Filter)] <- Implicitly applied when using [Filter(...)], do not use
                [ShowOn(TextAppearance.MeasureDescription)] <- Appears in measure description tables
                [ShowOn(TextAppearance.Notes)] <- Appears in the notes section in the data tool
        **/
        [Key]
        public int Index { get; set; }

        /** Modify below to resolve to valid data. These can also be adjusted from the generated models if you want them to be derived later **/
        [Title]
        public string Title => null;
        [CVInterpretation]
		public int CVInterpretation => int.Parse(ColCVInterpretation);
        [CVValue]
        public double? CVValue => double.TryParse(ColCV, out var result) ? result : (double?)null;
        [CVRangeUpper]
        public double? CVRangeUpper => null;
        [CVRangeLower]
        public double? CVRangeLower => null;
        [PointAverage]
        public double? PointAverage => double.TryParse(ColPercent, out var result) ? result : (double?)null;
        [PointUpper]
        public double? PointUpper => double.TryParse(ColCIUpper95, out var result) ? result : (double?)null;
        [PointLower]
        public double? PointLower => double.TryParse(ColCILow95, out var result) ? result : (double?)null;
        [UnitShort]
        public string UnitShort => ColLabel;
        [UnitLong]
        public string UnitLong => ColLabel;
        [DataLabelTable]
        public string DataLabelTable => ColDisaggregation;
        [DataLabelChart]
        public string DataLabelChart => ColDisaggregation;
        
        [Include]
        public bool Include => true;


        /** Modify below to mark filters with their levels and text to display**/
        [Text("Obs", "en-ca")]
        [Text("Obs", "fr-ca")]
        [CSVColumn("Obs")]
        public string ColObs { get; set; }
        
        
        [Text("Domain", "en-ca")]
        [Text("Domain", "fr-ca")]
        [CSVColumn("Domain")]
		[Filter(0)]
        public string ColDomain { get; set; }
        
        
        [Text("Indicator", "en-ca")]
        [Text("Indicator", "fr-ca")]
        [CSVColumn("Indicator")]
		[Filter(1)]
        public string ColIndicator { get; set; }
        
        
        [Text("Measures", "en-ca")]
        [Text("Measures", "fr-ca")]
        [CSVColumn("Measures")]
		[Filter(2)]
        public string ColMeasures { get; set; }
        
        
        [Text("Data Breakdowns", "en-ca")]
        [Text("Data Breakdowns", "fr-ca")]
        [CSVColumn("Data Breakdowns")]
		[Filter(3)]
        public string ColDataBreakdowns { get; set; }
        
        
        [Text("Disaggregation", "en-ca")]
        [Text("Disaggregation", "fr-ca")]
        [CSVColumn("Disaggregation")]
		[Filter(4)]
        public string ColDisaggregation { get; set; }
        
        
        [Text("nObs", "en-ca")]
        [Text("nObs", "fr-ca")]
        [CSVColumn("nObs")]
        public string ColNObs { get; set; }
        
        
        [Text("Percent", "en-ca")]
        [Text("Percent", "fr-ca")]
        [CSVColumn("Percent")]
        public string ColPercent { get; set; }
        
        
        [Text("CI_low_95", "en-ca")]
        [Text("CI_low_95", "fr-ca")]
        [CSVColumn("CI_low_95")]
        public string ColCILow95 { get; set; }
        
        
        [Text("CI_Upper_95", "en-ca")]
        [Text("CI_Upper_95", "fr-ca")]
        [CSVColumn("CI_Upper_95")]
        public string ColCIUpper95 { get; set; }
        
        
        [Text("CV", "en-ca")]
        [Text("CV", "fr-ca")]
        [CSVColumn("CV")]
        public string ColCV { get; set; }
        
        
        [Text("CV_Interpretation", "en-ca")]
        [Text("CV_Interpretation", "fr-ca")]
        [CSVColumn("CV_Interpretation")]
        public string ColCVInterpretation { get; set; }
        
        
        [Text("Label_", "en-ca")]
        [Text("Label_", "fr-ca")]
        [CSVColumn("Label_")]
        public string ColLabel { get; set; }
        
        
        [Text("Source", "en-ca")]
        [Text("Source", "fr-ca")]
        [CSVColumn("Source")]
        public string ColSource { get; set; }
        
        
        [Text("Notes", "en-ca")]
        [Text("Notes", "fr-ca")]
        [CSVColumn("Notes")]
        public string ColNotes { get; set; }
        
        
        [Text("Population", "en-ca")]
        [Text("Population", "fr-ca")]
        [CSVColumn("Population")]
        public string ColPopulation { get; set; }
        
        
        [Text("Measure", "en-ca")]
        [Text("Measure", "fr-ca")]
        [CSVColumn("Measure")]
        public string ColMeasure { get; set; }
        
        
        [Text("Definition", "en-ca")]
        [Text("Definition", "fr-ca")]
        [CSVColumn("Definition")]
        public string ColDefinition { get; set; }
        
        
        [Text("Data Source", "en-ca")]
        [Text("Data Source", "fr-ca")]
        [CSVColumn("Data Source")]
        public string ColDataSource { get; set; }
        
        
        [Text("Data Available", "en-ca")]
        [Text("Data Available", "fr-ca")]
        [CSVColumn("Data Available")]
        public string ColDataAvailable { get; set; }
        
        
        [Text("Population2", "en-ca")]
        [Text("Population2", "fr-ca")]
        [CSVColumn("Population2")]
        public string ColPopulation2 { get; set; }
        
        
        [Text("Estimate Calculation", "en-ca")]
        [Text("Estimate Calculation", "fr-ca")]
        [CSVColumn("Estimate Calculation")]
        public string ColEstimateCalculation { get; set; }
        
        
        [Text("Additional Remarks", "en-ca")]
        [Text("Additional Remarks", "fr-ca")]
        [CSVColumn("Additional Remarks")]
        public string ColAdditionalRemarks { get; set; }
        
        
    }
}