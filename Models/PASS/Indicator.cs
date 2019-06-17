using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Infobase.Automation;

namespace Infobase.Models.PASS
{
    [TextData("Name")]
    [ChildOf(typeof(LifeCourse))]
    [ParentOf(typeof(Measure), true)]
    public class Indicator
    {
        public int IndicatorId { get; set; }
        public int LifeCourseId { get; set; }
        public int Index { get; set; }
        public virtual LifeCourse LifeCourse { get; set; }
        [InverseProperty("Indicator")]
        public virtual ICollection<Measure> Measures { get; set; }

        /* Text getters */
        public string IndicatorName(string culture)
        {
            switch (culture)
            {
                case "en-ca": return IndicatorNameEn;
                case "fr-ca": return IndicatorNameFr;
            }
            return "No culture";
        }
        public string IndicatorNameEn { get; set; }
        public string IndicatorNameFr { get; set; }

        public int? DefaultMeasureId { get; set; }
        [ForeignKey("DefaultMeasureId")]
        public virtual Measure DefaultMeasure { get; set; }
    }

}
