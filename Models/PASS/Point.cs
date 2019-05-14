using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infobase.Models.PASS
{
    public class Point
    {
        public int PointId { get; set; }
        public int StrataId { get; set; }
        public int Index {get;set;}
        public double? ValueAverage { get; set; }
        public double? ValueUpper { get; set; }
        public double? ValueLower { get; set; }
        public int CVInterpretation { get; set; }
        public int? CVValue { get; set; }
        public virtual Strata Strata { get; set; }

        /* Text getters */
        public string PointLabelEn { get; set; }
        public string PointLabelFr { get; set; }
        

        public int Type { get; set; }
    }
}
