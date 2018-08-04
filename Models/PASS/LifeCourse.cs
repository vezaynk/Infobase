using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Infobase.Models.PASS
{
    public class LifeCourse
    {
        public int LifeCourseId { get; set; }
        public int IndicatorGroupId { get; set; }
        public virtual IndicatorGroup IndicatorGroup { get; set; }
        public virtual ICollection<LifeCourseNameTranslation> LifeCourseNameTranslations { get; set; }
        [InverseProperty("LifeCourse")]
        public virtual ICollection<Indicator> Indicators { get; set; }

        /* Text getters */
        public Translatable LifeCourseName => Translation.GetTranslation(LifeCourseNameTranslations);

        public int? DefaultIndicatorId { get; set; }
        [ForeignKey("DefaultIndicatorId")]
        public virtual Indicator DefaultIndicator { get; set; }
    }

    public class LifeCourseNameTranslation : ITranslation
    {
        public int TranslationId { get; set; }
        public virtual Translation Translation { get; set; }
        public int LifeCourseId { get; set; }
        public virtual LifeCourse LifeCourse { get; set; }
    }
}
