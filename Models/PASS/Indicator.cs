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
        public string GetIndicatorName(string lc, bool useLong=true)
        {
            return IndicatorNameTranslations.Where(t => t.Translation.LanguageCode == lc).Select(t => useLong ? t.Translation.Long : t.Translation.Short).FirstOrDefault();
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
