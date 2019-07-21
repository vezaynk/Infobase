using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using metadata_annotations;

namespace Infobase.Models.PASS
{
    [Filter(6)]
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

        public string PointLabel(string culture) {
            switch (culture) {
                case "en-ca": return PointLabelEn;
                case "fr-ca": return PointLabelFr;
            }
            return "No culture";
        }
        [CSVColumn("Strata")]
        public string PointLabelEn { get; set; }
        public string PointLabelFr { get; set; }
        

        public string PointText(string culture) {
            switch (culture) {
                case "en-ca": return PointTextEn;
                case "fr-ca": return PointTextFr;
            }
            return "No culture";
        }
        public string PointTextEn { get; set; }
        public string PointTextFr { get; set; }
        

        public int Type { get; set; }
    }
}
