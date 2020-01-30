// This file was written by a tool
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.Metadata;
using System.ComponentModel.DataAnnotations;

namespace Models.Contexts.PASS {
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
        /* [CVInterpretation]
		public int CVInterpretation => int.Parse(ColCVInterpretation); */
        [CVInterpretation]
		public int CVInterpretation => int.TryParse(ColCVInterpretation, out var result) ? result : (int)0;
        [CVValue]
        public double? CVValue => double.TryParse(ColCV1, out var result) ? result : (double?)null;
        [CVRangeUpper]
        public double? CVRangeUpper => double.TryParse(ColCVRange2, out var result) ? result : (double?)null;
        [CVRangeLower]
        public double? CVRangeLower => double.TryParse(ColCVRange1, out var result) ? result : (double?)null;
        [PointAverage]
        public double? PointAverage => double.TryParse(ColData, out var result) ? result : (double?)null;
        [PointUpper]
        public double? PointUpper => double.TryParse(ColCIUpper95, out var result) ? result : (double?)null;
        [PointLower]
        public double? PointLower => double.TryParse(ColCILow95, out var result) ? result : (double?)null;
        [UnitShort]
        public string UnitShort => ColUnitLabel2;
        [UnitLong]
        public string UnitLong => ColUnitLabel1;
        [DataLabelTable]
        public string DataLabelTable => ColDisaggregation;
        [DataLabelChart]
        public string DataLabelChart => ColDisaggregation;
        [Aggregator]
        public bool Aggregator => false;
        [Include]
        public bool Include => ColIncludeDT == "Y";


        /** Modify below to mark filters with their levels and text to display**/
        [Text("Activity", "en-ca")]
        [Text("Activité", "fr-ca")]
        [CSVColumn("Activity")]
		[Filter(0)]
        public string ColActivity { get; set; }
        
        
        [Text("Indicator Group", "en-ca")]
        [Text("Groupe d'indicateur", "fr-ca")]
        [CSVColumn("Indicator Group")]
		[Filter(1)]
        public string ColIndicatorGroup { get; set; }
        
        
        [Text("Life Course", "en-ca")]
        [Text("Parcours de vie", "fr-ca")]
        [CSVColumn("Life Course")]
		[Filter(2)]
        public string ColLifeCourse { get; set; }
        
        
        [Text("Indicators", "en-ca")]
        [Text("Indicateurs", "fr-ca")]
        [CSVColumn("Indicator")]
		[Filter(3)]
        public string ColIndicator { get; set; }
        
        
        [Text("Measures", "en-ca")]
        [Text("Mesures", "fr-ca")]
        [CSVColumn("Specific Measure 1")]
		[Filter(4)]
        public string ColSpecificMeasure1 { get; set; }
        
        
        [Text("Data Breakdowns", "en-ca")]
        [Text("Répartition des données", "fr-ca")]
        [CSVColumn("Data Breakdowns")]
		[Filter(5)]
        public string ColDataBreakdowns { get; set; }
        
        
        [Text("Disaggregation", "en-ca")]
        [Text("Disaggregation", "fr-ca")]
        [CSVColumn("Disaggregation")]
		[Filter(6)]
        public string ColDisaggregation { get; set; }
        
        
        [Text("nObs", "en-ca")]
        [Text("nObs", "fr-ca")]
        [CSVColumn("nObs")]
        public string ColNObs { get; set; }
        
        
        [Text("CV_1", "en-ca")]
        [Text("CV_1", "fr-ca")]
        [CSVColumn("CV_1")]
        public string ColCV1 { get; set; }
        
        
        [Text("StError", "en-ca")]
        [Text("StError", "fr-ca")]
        [CSVColumn("StError")]
        public string ColStError { get; set; }
        
        
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
        
        
        [Text("CV Range 1", "en-ca")]
        [Text("CV Range 1", "fr-ca")]
        [CSVColumn("CV Range 1")]
        public string ColCVRange1 { get; set; }
        
        
        [Text("CV Range 2", "en-ca")]
        [Text("CV Range 2", "fr-ca")]
        [CSVColumn("CV Range 2")]
        public string ColCVRange2 { get; set; }
        
        
        [Text("Display Data", "en-ca")]
        [Text("Display Data", "fr-ca")]
        [CSVColumn("Display Data")]
        public string ColDisplayData { get; set; }
        
        
        [Text("Population 1", "en-ca")]
        [Text("Population 1", "fr-ca")]
        [CSVColumn("Population 1")]
        public string ColPopulation1 { get; set; }
        
        
        [Text("Unit Label 1", "en-ca")]
        [Text("Unit Label 1", "fr-ca")]
        [CSVColumn("Unit Label 1")]
        public string ColUnitLabel1 { get; set; }
        
        
        [Text("Data Source 1", "en-ca")]
        [Text("Data Source 1", "fr-ca")]
        [CSVColumn("Data Source 1")]
        [ShowOn(TextAppearance.Notes)]
        public string ColDataSource1 { get; set; }
        
        
        [Text("Notes", "en-ca")]
        [Text("Notes", "fr-ca")]
        [CSVColumn("Notes")]
        [ShowOn(TextAppearance.Notes)]
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
        [ShowOn(TextAppearance.Notes)]
        public string ColDataSource2 { get; set; }
        
        
        [Text("Specific Measure 2", "en-ca")]
        [Text("Specific Measure 2", "fr-ca")]
        [CSVColumn("Specific Measure 2")]
        public string ColSpecificMeasure2 { get; set; }
        
        
        [Text("Definition", "en-ca")]
        [Text("Definition", "fr-ca")]
        [CSVColumn("Defintion")]
        [ShowOn(TextAppearance.MeasureDescription)]
        public string ColDefinition { get; set; }
        
        
        [Text("Data Source 3", "en-ca")]
        [Text("Data Source 3", "fr-ca")]
        [CSVColumn("Data Source 3")]
        [ShowOn(TextAppearance.Notes)]
        public string ColDataSource3 { get; set; }
        
        
        [Text("Data Available", "en-ca")]
        [Text("Data Available", "fr-ca")]
        [CSVColumn("Data Available")]
        [ShowOn(TextAppearance.MeasureDescription)]
        public string ColDataAvailable { get; set; }
        
        
        [Text("Population 2", "en-ca")]
        [Text("Population 2", "fr-ca")]
        [CSVColumn("Population 2")]
        public string ColPopulation2 { get; set; }
        
        
        [Text("Estimate Calculation", "en-ca")]
        [Text("Estimate Calculation", "fr-ca")]
        [CSVColumn("Estimate Calculation")]
        [ShowOn(TextAppearance.MeasureDescription)]
        public string ColEstimateCalculation { get; set; }
        
        
        [Text("Additional Remarks", "en-ca")]
        [Text("Additional Remarks", "fr-ca")]
        [CSVColumn("Additional Remarks")]
        [ShowOn(TextAppearance.MeasureDescription)]
        public string ColAdditionalRemarks { get; set; }
        
        
        [Text("Include_DT", "en-ca")]
        [Text("Include_DT", "fr-ca")]
        [CSVColumn("Include_DT")]
        public string ColIncludeDT { get; set; }
        
        
        [Text("Other DT Display", "en-ca")]
        [Text("Other DT Display", "fr-ca")]
        [CSVColumn("Other DT Display")]
        public string ColOtherDTDisplay { get; set; }
        
        
        [Text("2019 Updates (Y)", "en-ca")]
        [Text("2019 Updates (Y)", "fr-ca")]
        [CSVColumn("2019 Updates (Y)")]
        public string Col2019UpdatesY { get; set; }
        
        
    }
}