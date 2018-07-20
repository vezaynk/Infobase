using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactDotNetDemo.Models.PASS
{
    public class Translation
    {
        public int TranslationId { get; set; }
        public string LanguageCode { get; set; }
        public string Text { get; set; }

        /* Bunch of boilerplate to enable many-to-many bindings */
        public virtual ICollection<ActivityNameTranslation> ActivityNameTranslations { get; set; }
        public virtual ICollection<ActivityDescriptionTranslation> ActivityDescriptionTranslations { get; set; }
        public virtual ICollection<IndicatorNameTranslation> IndicatorNameTranslations { get; set; }
        public virtual ICollection<IndicatorGroupNameTranslation> IndicatorGroupNameTranslations { get; set; }
        public virtual ICollection<StrataNotesTranslation> StrataNotesTranslations { get; set; }
        public virtual ICollection<MeasureUnitTranslation> MeasureUnitTranslations { get; set; }
        public virtual ICollection<MeasureDefinitionTranslation> MeasureDefinitionTranslations { get; set; }
        public virtual ICollection<StrataSourceTranslation> StrataSourceTranslations { get; set; }
        public virtual ICollection<MeasureNameTranslation> MeasureNameTranslations { get; set; }
        public virtual ICollection<LifeCourseNameTranslation> LifeCourseNameTranslations { get; set; }

        public virtual ICollection<PointLabelTranslation> PointLabelTranslations { get; set; }
        public virtual ICollection<StrataNameTranslation> StrataNameTranslations { get; set; }
        public virtual ICollection<StrataPopulationTranslation> StrataPopulationTranslations { get; set; }

    }
}
