using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Infobase.Models.PASS
{
    public class IndicatorGroup
    {
        public int IndicatorGroupId { get; set; }
        public int ActivityId { get; set; }
        public int Index {get;set;}
        public virtual Activity Activity { get; set; }
        
        public virtual ICollection<IndicatorGroupNameTranslation> IndicatorGroupNameTranslations { get; set; }
        [InverseProperty("IndicatorGroup")]
        public virtual ICollection<LifeCourse> LifeCourses { get; set; }

        public Translatable IndicatorGroupName => Translation.GetTranslation(IndicatorGroupNameTranslations);
        public int? DefaultLifeCourseId { get; set; }
        [ForeignKey("DefaultLifeCourseId")]
        public virtual LifeCourse DefaultLifeCourse { get; set; }
    }

    public class IndicatorGroupNameTranslation: ITranslation
    {
        public int IndicatorGroupId { get; set; }
        public virtual IndicatorGroup IndicatorGroup { get; set; }
        public int TranslationId { get; set; }
        public virtual Translation Translation { get; set; }
    }
}
