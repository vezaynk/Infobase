// This file was written by a tool
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.Metadata;
using System.ComponentModel.DataAnnotations;

namespace Models.Contexts.HIV2 {
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

                [AggregatorLabel] (Label to use for aggregation stack, in lieu of the data point's label)
                [AggregatorReference] (Hidden, used cross-referencing aggregators)

                [ChartType] (Indicates the type of chart to use)
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
        public int CVInterpretation => 0;
        [CVValue]
        public double? CVValue => null;
        [CVRangeUpper]
        public double? CVRangeUpper => null;
        [CVRangeLower]
        public double? CVRangeLower => null;
        [PointAverage]
        public double? PointAverage => double.TryParse(ColValue, out var result) ? result : (double?)null;
        [PointUpper]
        public double? PointUpper => null;
        [PointLower]
        public double? PointLower => null;
        [UnitShort]
        public string UnitShort => "%";
        [UnitLong]
        public string UnitLong => "Percentage (%)";
        [DataLabelTable]
        public string DataLabelTable => $"{ColIndicatorBreakdown} - {ColSecondaryBreakdown}";
        [DataLabelChart]
        public string DataLabelChart => $"{ColIndicatorBreakdown} - {ColSecondaryBreakdown}";
        [AggregatorLabel]
        public string AggregatorLabel => ColIndicatorBreakdown;
        [AggregatorReference]
        public string AggregatorReference => ColSecondaryBreakdown;
        [ChartType]
        public ChartType ChartType {
            get {
                if (ColSecondaryBreakdownName == "None")
                    return ChartType.Bar;
                return ChartType.Stack;
            }
        }
        [Include]
        public bool Include => true;
        [Type]
        public int Type => 0;


        /** Modify below to mark filters with their levels and text to display**/
        [Text("Indicator Parent", "en-ca")]
        [Text("Indicator Parent", "fr-ca")]
        [CSVColumn("Indicator Parent")]
        [Filter(0)]
        public string ColIndicatorParent { get; set; }
        
        
        [Text("Indicator Breakdown", "en-ca")]
        [Text("Indicator Breakdown", "fr-ca")]
        [CSVColumn("Indicator Breakdown")]
        public string ColIndicatorBreakdown { get; set; }
        
        
        [Text("Secondary Breakdown", "en-ca")]
        [Text("Secondary Breakdown", "fr-ca")]
        [CSVColumn("Secondary Breakdown")]
        public string ColSecondaryBreakdown { get; set; }

        [Text("Secondary Breakdown Name", "en-ca")]
        [Text("Secondary Breakdown Name", "fr-ca")]
        [CSVColumn("Secondary Breakdown Name")]
        [Filter(1)]
        public string ColSecondaryBreakdownName { get; set; }
        
        [Text("Location", "en-ca")]
        [Text("Location", "fr-ca")]
        [CSVColumn("Location")]
        [Filter(2)]
        public string ColLocation { get; set; }
        
        
        [Text("Value", "en-ca")]
        [Text("Value", "fr-ca")]
        [CSVColumn("Value")]
        
        [Filter(3)]
        public string ColValue { get; set; }
        
        
    }
}