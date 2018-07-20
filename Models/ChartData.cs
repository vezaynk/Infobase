using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactDotNetDemo.Models
{
    public class ChartData
    {
        public class Labels
        {
            public string x;
            public string y;
        }
        public class Values
        {
            public List<Point> points;

            public int type;
        }
        public class Point
        {
            public double? value;
            public CV cv;
            public string label;
            public class Interval
            {
                public double upper;
                public double lower;
            }

            public class CV
            {
                public double value;
                public int interpretation;
            }

            public Interval confidence;
        }

        public enum Scale
        {
            Absolute, Percent, Sequence
        }

        public Labels axis;
        public List<Values> values;
        public string title;
        public string source;
        public string population;
        public string notes;
        public MeasureDescription measure;
        public class MeasureDescription
        {
            public string definition;
            public string dataAvailable;
            public string method;
            public string additionalNotes;
        }
    }
}
