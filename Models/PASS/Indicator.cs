using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ReactDotNetDemo.Models.PASS
{
    public class Indicator
    {
        public int IndicatorId { get; set; }
        public int LifeCourseId { get; set; }
        public virtual LifeCourse LifeCourse { get; set; }
        public virtual ICollection<IndicatorNameTranslation> IndicatorNameTranslations { get; set; }
        [InverseProperty("Indicator")]
        public virtual ICollection<Measure> Measures { get; set; }

        /* Text getters */
        public Translatable IndicatorName => Translation.GetTranslation(IndicatorNameTranslations);

        public int? DefaultMeasureId { get; set; }
        [ForeignKey("DefaultMeasureId")]
        public virtual Measure DefaultMeasure { get; set; }
    }

    public class IndicatorNameTranslation : ITranslation
    {
        public int TranslationId { get; set; }
        public virtual Translation Translation { get; set; }
        public int IndicatorId { get; set; }
        public virtual Indicator Indicator { get; set; }
    }
    
}
