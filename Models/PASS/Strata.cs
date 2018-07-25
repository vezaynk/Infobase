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
        public virtual ICollection<StrataNotesTranslation> StrataNotesTranslations { get; set; }

        public virtual ICollection<Point> Points { get; set; }
        /* Text getters */
        public string GetStrataNotes(string lc, string type) => Translation.GetTranslation((ICollection<ITranslation>)StrataNotesTranslations, lc, null);
        public string GetStrataName(string lc, string type) => Translation.GetTranslation((ICollection<ITranslation>)StrataNameTranslations, lc, null);
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
}
