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
        public string Type { get; set; }
        public string Text { get; set; }

        /* Bunch of boilerplate to enable many-to-many bindings */
        public virtual ICollection<ActivityNameTranslation> ActivityNameTranslations { get; set; }
        public virtual ICollection<ActivityDescriptionTranslation> ActivityDescriptionTranslations { get; set; }
        public virtual ICollection<IndicatorNameTranslation> IndicatorNameTranslations { get; set; }
        public virtual ICollection<IndicatorGroupNameTranslation> IndicatorGroupNameTranslations { get; set; }
        public virtual ICollection<StrataNotesTranslation> StrataNotesTranslations { get; set; }
        public virtual ICollection<MeasureUnitTranslation> MeasureUnitTranslations { get; set; }
        public virtual ICollection<MeasureDefinitionTranslation> MeasureDefinitionTranslations { get; set; }
        public virtual ICollection<MeasureSourceTranslation> MeasureSourceTranslations { get; set; }
        public virtual ICollection<MeasureNameTranslation> MeasureNameTranslations { get; set; }
        public virtual ICollection<LifeCourseNameTranslation> LifeCourseNameTranslations { get; set; }

        public virtual ICollection<PointLabelTranslation> PointLabelTranslations { get; set; }
        public virtual ICollection<StrataNameTranslation> StrataNameTranslations { get; set; }
        public virtual ICollection<MeasurePopulationTranslation> MeasurePopulationTranslations { get; set; }

        // Simplified process for getting wanted translation
        public static string GetTranslation(ICollection<ITranslation> TranslationTable, string lc, string type)
        {
            return TranslationTable
                    .Where(t => (type == null || t.Translation.Type == type) 
                                && t.Translation.LanguageCode == lc)
                                        .Select(t => t.Translation.Text)
                                                .FirstOrDefault();
        }
    }

    public interface ITranslation
    {
        int TranslationId { get; set; }
        Translation Translation { get; set; }
    }
}
