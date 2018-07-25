using System;
using System.Collections.Generic;
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
        public virtual ICollection<Measure> Measures { get; set; }

        /* Text getters */
        public string GetIndicatorName(string lc, string type) => Translation.GetTranslation((ICollection<ITranslation>)IndicatorNameTranslations, lc, null);
    }

    public class IndicatorNameTranslation : ITranslation
    {
        public int TranslationId { get; set; }
        public virtual Translation Translation { get; set; }
        public int IndicatorId { get; set; }
        public virtual Indicator Indicator { get; set; }
    }
    
}
