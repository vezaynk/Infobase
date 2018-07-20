using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactDotNetDemo.Models.PASS
{
    public class Strata
    {
        public int StrataId { get; set; }
        public int MeasureId { get; set; }
        public virtual Measure Measure { get; set; }
        public virtual ICollection<StrataNameTranslation> StrataNameTranslations { get; set; }
        public virtual ICollection<StrataPopulationTranslation> StrataPopulationTranslations { get; set; }
        public virtual ICollection<StrataSourceTranslation> StrataSourceTranslations { get; set; }
        public virtual ICollection<StrataNotesTranslation> StrataNotesTranslations { get; set; }

        public virtual ICollection<Point> Points { get; set; }
        /* Text getters */
        public string GetStrataNotes(string lc)
        {
            return StrataNotesTranslations.Where(t => t.Translation.LanguageCode == lc).Select(t => t.Translation.Text).FirstOrDefault();
        }
        public string GetStrataSource(string lc)
        {
            return StrataSourceTranslations.Where(t => t.Translation.LanguageCode == lc).Select(t => t.Translation.Text).FirstOrDefault();
        }
        public string GetStrataPopulation(string lc)
        {
            return StrataPopulationTranslations.Where(t => t.Translation.LanguageCode == lc)
                .Select(t => t.Translation.Text)
                .FirstOrDefault();
        }
        public string GetStrataName(string lc)
        {
            return StrataNameTranslations.Where(t => t.Translation.LanguageCode == lc).Select(t => t.Translation.Text).FirstOrDefault();
        }
    }
    

    public class StrataNameTranslation
    {
        public int TranslationId { get; set; }
        public virtual Translation Translation { get; set; }
        public int StrataId { get; set; }
        public virtual Strata Strata { get; set; }
    }

    public class StrataPopulationTranslation
    {
        public int TranslationId { get; set; }
        public virtual Translation Translation { get; set; }
        public int StrataId { get; set; }
        public virtual Strata Strata { get; set; }
    }

    public class StrataSourceTranslation
    {
        public int TranslationId { get; set; }
        public virtual Translation Translation { get; set; }
        public int StrataId { get; set; }
        public virtual Strata Strata { get; set; }
    }


    public class StrataNotesTranslation
    {
        public int TranslationId { get; set; }
        public virtual Translation Translation { get; set; }
        public int StrataId { get; set; }
        public virtual Strata Strata { get; set; }
    }
}
