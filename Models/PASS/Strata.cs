using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public virtual ICollection<StrataNotesTranslation> StrataNotesTranslations { get; set; }
        public virtual ICollection<StrataSourceTranslation> StrataSourceTranslations { get; set; }
        public virtual ICollection<StrataPopulationTranslation> StrataPopulationTranslations { get; set; }
        [InverseProperty("Strata")]
        public virtual ICollection<Point> Points { get; set; }
        /* Text getters */
        public Translatable StrataNotes => Translation.GetTranslation(StrataNotesTranslations);
        public Translatable StrataName => Translation.GetTranslation(StrataNameTranslations);
        public Translatable StrataSource => Translation.GetTranslation(StrataSourceTranslations);
        public Translatable StrataPopulation => Translation.GetTranslation(StrataPopulationTranslations);
    }
    

    public class StrataNameTranslation : ITranslation
    {
        public int TranslationId { get; set; }
        public virtual Translation Translation { get; set; }
        public int StrataId { get; set; }
        public virtual Strata Strata { get; set; }
    }


    public class StrataNotesTranslation : ITranslation
    {
        public int TranslationId { get; set; }
        public virtual Translation Translation { get; set; }
        public int StrataId { get; set; }
        public virtual Strata Strata { get; set; }
    }

    public class StrataSourceTranslation : ITranslation
    {
        public int TranslationId { get; set; }
        public virtual Translation Translation { get; set; }
        public int StrataId { get; set; }
        public virtual Strata Strata { get; set; }
    }

    public class StrataPopulationTranslation : ITranslation
    {
        public int TranslationId { get; set; }
        public virtual Translation Translation { get; set; }
        public int StrataId { get; set; }
        public virtual Strata Strata { get; set; }
    }
}
