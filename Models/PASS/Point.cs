using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactDotNetDemo.Models.PASS
{
    public class Point
    {
        public int PointId { get; set; }
        public int StrataId { get; set; }
        public double? ValueAverage { get; set; }
        public double? ValueUpper { get; set; }
        public double? ValueLower { get; set; }
        public int CVInterpretation { get; set; }
        public virtual Strata Strata { get; set; }
        public virtual ICollection<PointLabelTranslation> PointLabelTranslations { get; set; }

        /* Text getters */
        public string GetPointLabel(string lc)
        {
            return PointLabelTranslations.Where(t => t.Translation.LanguageCode == lc).Select(t => t.Translation.Text).FirstOrDefault();
        }
    }

    public class PointLabelTranslation
    {
        public int TranslationId { get; set; }
        public virtual Translation Translation { get; set; }
        public int PointId { get; set; }
        public virtual Point Point { get; set; }
    }
}
