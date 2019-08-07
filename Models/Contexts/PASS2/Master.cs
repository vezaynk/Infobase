// This file was written by a tool
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.Metadata;
using System.ComponentModel.DataAnnotations;

namespace Models.Contexts.PASS2 {
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
        [CVInterpretation]
        public int CVInterpretation => 0;
        [CVValue]
        public double? CVValue => null;
        [CVRangeUpper]
        public double? CVRangeUpper => null;
        [CVRangeLower]
        public double? CVRangeLower => null;
        [PointAverage]
        public double? PointAverage => null;
        [PointUpper]
        public double? PointUpper => null;
        [PointLower]
        public double? PointLower => null;
        [UnitShort]
        public string UnitShort => null;
        [UnitLong]
        public string UnitLong => null;
        [DataLabelTable]
        public string DataLabelTable => null;
        [DataLabelChart]
        public string DataLabelChart => null;
        [Aggregator]
        public bool Aggregator => false;
        [Include]
        public bool Include => ColIsIncluded == "Y";


        /** Modify below to mark filters with their levels and text to display**/
        [Text("Activity", "en-ca")]
        [Text("Activity", "fr-ca")]
        [CSVColumn("Activity")]
        [Filter(0)]
        public string ColActivity { get; set; }
        
        
        [Text("Indicator Group", "en-ca")]
        [Text("Indicator Group", "fr-ca")]
        [CSVColumn("Indicator Group")]
        [Filter(1)]
        public string ColIndicatorGroup { get; set; }
        
        
        [Text("Life Course", "en-ca")]
        [Text("Life Course", "fr-ca")]
        [CSVColumn("Life Course")]
        [Filter(2)]
        public string ColLifeCourse { get; set; }
        
        
        [Text("Indicator", "en-ca")]
        [Text("Indicator", "fr-ca")]
        [CSVColumn("Indicator")]
        [Filter(3)]
        public string ColIndicator { get; set; }
        
        
        [Text("Specific Measure", "en-ca")]
        [Text("Specific Measure", "fr-ca")]
        [CSVColumn("Specific Measure")]
        [Filter(4)]
        public string ColSpecificMeasure { get; set; }
        
        
        [Text("Data Breakdowns", "en-ca")]
        [Text("Data Breakdowns", "fr-ca")]
        [CSVColumn("Data Breakdowns")]
        [Filter(5)]
        public string ColDataBreakdowns { get; set; }
        
        
        [Text("Strata", "en-ca")]
        [Text("Strata", "fr-ca")]
        [CSVColumn("Strata")]
        [Filter(6)]
        public string ColStrata { get; set; }
        
        
        [Text("CV", "en-ca")]
        [Text("CV", "fr-ca")]
        [CSVColumn("CV")]
        public string ColCV { get; set; }
        
        
        [Text("Data", "en-ca")]
        [Text("Data", "fr-ca")]
        [CSVColumn("Data")]
        public string ColData { get; set; }
        
        
        [Text("CI_low_95", "en-ca")]
        [Text("CI_low_95", "fr-ca")]
        [CSVColumn("CI_low_95")]
        public string ColCILow95 { get; set; }
        
        
        [Text("CI_Upper_95", "en-ca")]
        [Text("CI_Upper_95", "fr-ca")]
        [CSVColumn("CI_Upper_95")]
        public string ColCIUpper95 { get; set; }
        
        
        [Text("CV_Interpretation", "en-ca")]
        [Text("CV_Interpretation", "fr-ca")]
        [CSVColumn("CV_Interpretation")]
        public string ColCVInterpretation { get; set; }
        
        
        [Text("CV Range Lower", "en-ca")]
        [Text("CV Range Lower", "fr-ca")]
        [CSVColumn("CV Range Lower")]
        public string ColCVRangeLower { get; set; }
        
        
        [Text("CV Range Upper", "en-ca")]
        [Text("CV Range Upper", "fr-ca")]
        [CSVColumn("CV Range Upper")]
        public string ColCVRangeUpper { get; set; }
        
        
        [Text("Feature Data", "en-ca")]
        [Text("Feature Data", "fr-ca")]
        [CSVColumn("Feature Data")]
        public string ColFeatureData { get; set; }
        
        
        [Text("Population 1", "en-ca")]
        [Text("Population 1", "fr-ca")]
        [CSVColumn("Population 1")]
        public string ColPopulation1 { get; set; }
        
        
        [Text("Unit Label Long", "en-ca")]
        [Text("Unit Label Long", "fr-ca")]
        [CSVColumn("Unit Label Long")]
        public string ColUnitLabelLong { get; set; }
        
        
        [Text("Data Source 1", "en-ca")]
        [Text("Data Source 1", "fr-ca")]
        [CSVColumn("Data Source 1")]
        public string ColDataSource1 { get; set; }
        
        
        [Text("Notes", "en-ca")]
        [Text("Notes", "fr-ca")]
        [CSVColumn("Notes")]
        public string ColNotes { get; set; }
        
        
        [Text("PT Table Label", "en-ca")]
        [Text("PT Table Label", "fr-ca")]
        [CSVColumn("PT Table Label")]
        public string ColPTTableLabel { get; set; }
        
        
        [Text("Unit Label 2", "en-ca")]
        [Text("Unit Label 2", "fr-ca")]
        [CSVColumn("Unit Label 2")]
        public string ColUnitLabel2 { get; set; }
        
        
        [Text("Data Source 2", "en-ca")]
        [Text("Data Source 2", "fr-ca")]
        [CSVColumn("Data Source 2")]
        public string ColDataSource2 { get; set; }
        
        
        [Text("Specific Measure 2", "en-ca")]
        [Text("Specific Measure 2", "fr-ca")]
        [CSVColumn("Specific Measure 2")]
        public string ColSpecificMeasure2 { get; set; }
        
        
        [Text("Defintion", "en-ca")]
        [Text("Defintion", "fr-ca")]
        [CSVColumn("Defintion")]
        public string ColDefintion { get; set; }
        
        
        [Text("Data Source 3", "en-ca")]
        [Text("Data Source 3", "fr-ca")]
        [CSVColumn("Data Source 3")]
        public string ColDataSource3 { get; set; }
        
        
        [Text("Data Available", "en-ca")]
        [Text("Data Available", "fr-ca")]
        [CSVColumn("Data Available")]
        public string ColDataAvailable { get; set; }
        
        
        [Text("Population 2", "en-ca")]
        [Text("Population 2", "fr-ca")]
        [CSVColumn("Population 2")]
        public string ColPopulation2 { get; set; }
        
        
        [Text("Estimate Calculation", "en-ca")]
        [Text("Estimate Calculation", "fr-ca")]
        [CSVColumn("Estimate Calculation")]
        public string ColEstimateCalculation { get; set; }
        
        
        [Text("Additional Remarks", "en-ca")]
        [Text("Additional Remarks", "fr-ca")]
        [CSVColumn("Additional Remarks")]
        public string ColAdditionalRemarks { get; set; }
        
        
        [Text("Is Included", "en-ca")]
        [Text("Is Included", "fr-ca")]
        [CSVColumn("Is Included")]
        public string ColIsIncluded { get; set; }
        
        
        [Text("Is Aggregator", "en-ca")]
        [Text("Is Aggregator", "fr-ca")]
        [CSVColumn("Is Aggregator")]
        public string ColIsAggregator { get; set; }
        
        
    }
}