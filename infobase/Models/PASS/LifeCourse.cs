using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using metadata_annotations;

namespace Infobase.Models.PASS
{
    [Text("Life Course", "en-ca")]
    [Filter(2)]
    public class LifeCourse
    {
        public int LifeCourseId { get; set; }
        public int IndicatorGroupId { get; set; }
        public int Index {get;set;}
        public virtual IndicatorGroup IndicatorGroup { get; set; }
        [InverseProperty("LifeCourse")]
        public virtual ICollection<Indicator> Indicators { get; set; }

        /* Text getters */
        public string LifeCourseName(string culture) {
            switch (culture) {
                case "en-ca": return LifeCourseNameEn;
                case "fr-ca": return LifeCourseNameFr;
            }
            return "No culture";
        }
        [CSVColumn("Life Course")]
        public string LifeCourseNameEn {get; set;}
        public string LifeCourseNameFr {get; set;}

        public int? DefaultIndicatorId { get; set; }
        [ForeignKey("DefaultIndicatorId")]
        public virtual Indicator DefaultIndicator { get; set; }
    }
}
