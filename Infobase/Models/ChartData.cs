using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infobase.Models
{
    public class Point
    {
        public double? Value { get; set; }
        public double? ValueUpper { get; set; }
        public double? ValueLower { get; set; }
        public string Label { get; set; }
        public string Text { get; set; }
        public double? CVValue { get; set; }
        public int CVInterpretation { get; set; }
        public int Type { get; set; }
    }
    public enum ChartType { Bar, Trend };
    public class ChartData
    {
        public string XAxis { get; set; }
        public string YAxis { get; set; }
        public string Unit { get; set; }
        public IEnumerable<Point> Points { get; set; }
        public double? WarningCV { get; set; }
        public double? SuppressCV { get; set; }
        public string Title { get; set; }
        public ChartType ChartType { get; set; }
        public dynamic DescriptionTable { get; set; }
        public dynamic Notes { get; set; }
    }
}
