// This file was written by a tool
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.Metadata;
using System.ComponentModel.DataAnnotations;

namespace Models.Contexts.PASS2
{
    public class Master
    {
        /**
            It is important to not forget to use the following annotations also (Cut and paste as necessary).`
            If the needed data column is derivable, include it in the generate data models later.

            ** Data Properties **
                [CVRangeUpper]
                [CVRangeLower]
                [CV]
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
                [Text("Visible Text", "en-ca/fr-ca")] <- This text will be show alongside wherever the contents appear. (i.e. headings for a table)
                [ShowOn(TextAppearance.Filter)] <- Implicitly applied when using [Filter(...)], do not use
                [ShowOn(TextAppearance.MeasureDescription)] <- Appears in measure description tables
                [ShowOn(TextAppearance.Notes)] <- Appears in the notes section in the data tool
        **/
        [Key]
        public int Index { get; set; }

        [Filter(0)]
        [Text("Activity", "en-ca")]
        [Text("Activity", "fr-ca")]
        [CSVColumn("Activity")]
        public string Activity { get; set; }

        [Filter(1)]
        [Text("Indicator Group", "en-ca")]
        [Text("Indicator Group", "fr-ca")]
        [CSVColumn("Indicator Group")]
        public string IndicatorGroup { get; set; }

        [Filter(2)]
        [Text("Life Course", "en-ca")]
        [Text("Life Course", "fr-ca")]
        [CSVColumn("Life Course")]
        public string LifeCourse { get; set; }

        [Filter(3)]

        [Text("Indicator", "en-ca")]
        [Text("Indicator", "fr-ca")]
        [CSVColumn("Indicator")]
        public string Indicator { get; set; }

        [Filter(4)]
        [Text("Specific Measure", "en-ca")]
        [Text("Specific Measure", "fr-ca")]
        [CSVColumn("Specific Measure")]
        public string SpecificMeasure { get; set; }

        [Filter(5)]
        [Text("Data Breakdowns", "en-ca")]
        [Text("Data Breakdowns", "fr-ca")]
        [CSVColumn("Data Breakdowns")]
        public string DataBreakdowns { get; set; }

        [Filter(6)]
        [Text("Strata", "en-ca")]
        [Text("Strata", "fr-ca")]
        [CSVColumn("Strata")]
        [DataLabelChart]
        public string Strata { get; set; }


        [Text("CV", "en-ca")]
        [Text("CV", "fr-ca")]
        [CSVColumn("CV")]
        [CVValue]
        public string CV { get; set; }


        [PointAverage]
        [Text("Data", "en-ca")]
        [Text("Data", "fr-ca")]
        [CSVColumn("Data")]
        public string Data { get; set; }

        [PointLower]
        [Text("CI_low_95", "en-ca")]
        [Text("CI_low_95", "fr-ca")]
        [CSVColumn("CI_low_95")]
        public string CILow95 { get; set; }

        [PointUpper]
        [Text("CI_Upper_95", "en-ca")]
        [Text("CI_Upper_95", "fr-ca")]
        [CSVColumn("CI_Upper_95")]
        public string CIUpper95 { get; set; }

        [CVInterpretation]
        [Text("CV_Interpretation", "en-ca")]
        [Text("CV_Interpretation", "fr-ca")]
        [CSVColumn("CV_Interpretation")]
        public string CVInterpretation { get; set; }


        [CVRangeLower]
        [Text("CV Range Lower", "en-ca")]
        [Text("CV Range Lower", "fr-ca")]
        [CSVColumn("CV Range Lower")]
        public string CVRangeLower { get; set; }

        [CVRangeUpper]
        [Text("CV Range Upper", "en-ca")]
        [Text("CV Range Upper", "fr-ca")]
        [CSVColumn("CV Range Upper")]
        public string CVRangeUpper { get; set; }

        [Text("Feature Data", "en-ca")]
        [Text("Feature Data", "fr-ca")]
        [CSVColumn("Feature Data")]
        public string FeatureData { get; set; }


        [Text("Population 1", "en-ca")]
        [Text("Population 1", "fr-ca")]
        [CSVColumn("Population 1")]
        public string Population1 { get; set; }


        [UnitLong]
        [Text("Unit Label Long", "en-ca")]
        [Text("Unit Label Long", "fr-ca")]
        [CSVColumn("Unit Label Long")]
        public string UnitLabelLong { get; set; }


        [UnitShort]
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
        [DataLabelTable]
        public string PTTableLabel { get; set; }


        [Text("Unit Label 2", "en-ca")]
        [Text("Unit Label 2", "fr-ca")]
        [CSVColumn("Unit Label 2")]
        [UnitShort]
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


        [Text("Is Included", "en-ca")]
        [Text("Is Included", "fr-ca")]
        [CSVColumn("Is Included")]
        [Included]
        public string IsIncluded { get; set; }


        [Text("Is Aggregator", "en-ca")]
        [Text("Is Aggregator", "fr-ca")]
        [CSVColumn("Is Aggregator")]
        public string IsAggregator { get; set; }


    }
}
