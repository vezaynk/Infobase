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
        public int? CVValue { get; set; }
        public virtual Strata Strata { get; set; }
        public virtual ICollection<PointLabelTranslation> PointLabelTranslations { get; set; }

        /* Text getters */
        public string GetPointLabel(string lc, string type) => Translation.GetTranslation( PointLabelTranslations, lc, null);

        public int Type { get; set; }
    }

    public class PointLabelTranslation : ITranslation
    {
        public int TranslationId { get; set; }
        public virtual Translation Translation { get; set; }
        public int PointId { get; set; }
        public virtual Point Point { get; set; }
    }
}
