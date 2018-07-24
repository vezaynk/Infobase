using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReactDotNetDemo.Models.PASS
{
    public class Activity
    {
        public int ActivityId { get; set; }
        public virtual ICollection<IndicatorGroup> IndicatorGroups { get; set; }
        public virtual ICollection<ActivityNameTranslation> ActivityNameTranslations { get; set; }
        public virtual ICollection<ActivityDescriptionTranslation> ActivityDescriptionTranslations { get; set; }
        /* Text getters */
        public string GetActivityName(string lc, bool useLong=true)
        {
            return ActivityNameTranslations.Where(t => t.Translation.LanguageCode == lc).Select(t => useLong ? t.Translation.Long : t.Translation.Short).FirstOrDefault();
        }
    }

    public class ActivityNameTranslation
    {
        public int TranslationId { get; set; }
        public virtual Translation Translation { get; set; }
        public int ActivityId { get; set; }
        public virtual Activity Activity { get; set; }
    }


    public class ActivityDescriptionTranslation
    {
        public int TranslationId { get; set; }
        public virtual Translation Translation { get; set; }
        public int ActivityId { get; set; }
        public virtual Activity Activity { get; set; }
    }

}
