using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Infobase.Automation;

namespace Infobase.Models.PASS
{
    [TextData("Name")]
    [ChildOf(typeof(Activity))]
    [ParentOf(typeof(LifeCourse), true)]
    public class IndicatorGroup
    {
        public int IndicatorGroupId { get; set; }
        public int ActivityId { get; set; }
        public int Index {get;set;}
        public virtual Activity Activity { get; set; }
        
        [InverseProperty("IndicatorGroup")]
        public virtual ICollection<LifeCourse> LifeCourses { get; set; }
        public string IndicatorGroupName(string culture) {
            switch (culture) {
                case "en-ca": return IndicatorGroupNameEn;
                case "fr-ca": return IndicatorGroupNameFr;
            }
            return "No culture";
        }
        public string IndicatorGroupNameEn { get; set; }
        public string IndicatorGroupNameFr { get; set; }
        public int? DefaultLifeCourseId { get; set; }
        [ForeignKey("DefaultLifeCourseId")]
        public virtual LifeCourse DefaultLifeCourse { get; set; }
    }
}
