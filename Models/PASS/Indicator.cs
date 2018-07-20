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
        public string GetIndicatorName(string lc)
        {
            return IndicatorNameTranslations.Where(t => t.Translation.LanguageCode == lc).Select(t => t.Translation.Text).FirstOrDefault();
        }
    }

    public class IndicatorNameTranslation
    {
        public int TranslationId { get; set; }
        public virtual Translation Translation { get; set; }
        public int IndicatorId { get; set; }
        public virtual Indicator Indicator { get; set; }
    }
    
}
