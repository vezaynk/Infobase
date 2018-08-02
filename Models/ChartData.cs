using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactDotNetDemo.Models
{
    public class ChartData
    {
        public class Point
        {
            public double? Value { get; set; }
            public double? ValueUpper { get; set; }
            public double? ValueLower { get; set; }
            public Translatable Label { get; set; }
            public double? CVValue { get; set; }
            public int CVInterpretation { get; set; }

        }


        public Translatable XAxis { get; set; }
        public Translatable YAxis { get; set; }
        public IEnumerable<Point> Points { get; set; }
        public Translatable Source { get; set; }
        public Translatable Organization { get; set; }
        public Translatable Population { get; set; }
        public Translatable Notes { get; set; }

        public Translatable Definition { get; set; }
        public Translatable DataAvailable { get; set; }
        public Translatable Method { get; set; }
        public Translatable Remarks { get; set; }

        public double? WarningCV {get;set;}
        public double? SuppressCV {get;set;}

        public Translatable MeasureName {get;set;}
    }
}
