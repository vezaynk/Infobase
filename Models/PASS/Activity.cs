using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Infobase.Models.PASS
{
    public class Activity
    {
        public int ActivityId { get; set; }
        public int Index {get;set;}
        [InverseProperty("Activity")]
        public virtual ICollection<IndicatorGroup> IndicatorGroups { get; set; }
        /* Text getters */
        public string ActivityNameEn { get; set; }
        public string ActivityNameFr { get; set; }

        public int? DefaultIndicatorGroupId { get; set; }
        [ForeignKey("DefaultIndicatorGroupId")]
        public virtual IndicatorGroup DefaultIndicatorGroup { get; set; }
    }

}
