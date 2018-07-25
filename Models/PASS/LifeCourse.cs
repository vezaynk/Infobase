using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactDotNetDemo.Models.PASS
{
    public class LifeCourse
    {
        public int LifeCourseId { get; set; }
        public int IndicatorGroupId { get; set; }
        public virtual IndicatorGroup IndicatorGroup { get; set; }
        public virtual ICollection<LifeCourseNameTranslation> LifeCourseNameTranslations { get; set; }
        public virtual ICollection<Indicator> Indicators { get; set; }

        /* Text getters */
        public string GetLifeCourseName(string lc, string type) => Translation.GetTranslation((ICollection<ITranslation>)LifeCourseNameTranslations, lc, null);
    }

    public class LifeCourseNameTranslation : ITranslation
    {
        public int TranslationId { get; set; }
        public virtual Translation Translation { get; set; }
        public int LifeCourseId { get; set; }
        public virtual LifeCourse LifeCourse { get; set; }
    }
}
