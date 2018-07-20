using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactDotNetDemo.Models.PASS
{
    public class Measure
    {
        public int MeasureId { get; set; }
        public int IndicatorId { get; set; }
        public virtual Indicator Indicator { get; set; }
        public virtual ICollection<MeasureUnitTranslation> MeasureUnitTranslations { get; set; }
        public virtual ICollection<MeasureDefinitionTranslation> MeasureDefinitionTranslations { get; set; }
        
       public virtual ICollection<MeasureNameTranslation> MeasureNameTranslations { get; set; }

        public virtual ICollection<Strata> Stratas { get; set; }

        public string GetMeasureName(string lc)
        {
            return MeasureNameTranslations.Where(t => t.Translation.LanguageCode == lc).Select(t => t.Translation.Text).FirstOrDefault();
        }
        public string GetMeasureDefinition(string lc)
        {
            return MeasureDefinitionTranslations.Where(t => t.Translation.LanguageCode == lc).Select(t => t.Translation.Text).FirstOrDefault();
        }
        public string GetMeasureUnit(string lc)
        {
            return MeasureUnitTranslations.Where(t => t.Translation.LanguageCode == lc).Select(t => t.Translation.Text).FirstOrDefault();
        }
    }

    public class MeasureNameTranslation
    {
        public int TranslationId { get; set; }
        public virtual Translation Translation { get; set; }
        public int MeasureId { get; set; }
        public virtual Measure Measure { get; set; }
    }
    public class MeasureUnitTranslation
    {
        public int TranslationId { get; set; }
        public virtual Translation Translation { get; set; }
        public int MeasureId { get; set; }
        public virtual Measure Measure { get; set; }
    }

    public class MeasureDefinitionTranslation
    {
        public int TranslationId { get; set; }
        public virtual Translation Translation { get; set; }
        public int MeasureId { get; set; }
        public virtual Measure Measure { get; set; }
    }
}
