﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using metadata_annotations;

namespace Infobase.Models.PASS
{
    [Filter(0)]
    [Text("Activity", "en-ca")]
    [Text("Activité", "fr-ca")]
    public class Activity
    {
        public int ActivityId { get; set; }
        public int Index { get; set; }
        [InverseProperty("Activity")]
        public virtual ICollection<IndicatorGroup> IndicatorGroups { get; set; }
        /* Text getters */
        public string ActivityName(string culture)
        {
            switch (culture)
            {
                case "en-ca": return ActivityNameEn;
                case "fr-ca": return ActivityNameFr;
            }
            return "No culture";
        }
        [CSVColumn("Activity")]
        public string ActivityNameEn { get; set; }
        public string ActivityNameFr { get; set; }

        public int? DefaultIndicatorGroupId { get; set; }
        [ForeignKey("DefaultIndicatorGroupId")]
        public virtual IndicatorGroup DefaultIndicatorGroup { get; set; }
    }

}