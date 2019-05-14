using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Infobase.Models.PASS
{
    public class Measure
    {
        public int MeasureId { get; set; }
        public int IndicatorId { get; set; }
        public int Index {get;set;}
        public virtual Indicator Indicator { get; set; }
        public bool Included { get; set; }
        public bool Aggregator { get; set; }
        public double? CVWarnAt { get; set; }
        public double? CVSuppressAt { get; set; }
        
        [InverseProperty("Measure")]
        public virtual ICollection<Strata> Stratas { get; set; }

        public string MeasureNameIndexEn {get; set;}
        public string MeasureNameDataToolEn {get; set;}
        public string MeasureDefinitionEn {get; set;}
        public string MeasureMethodEn {get; set;}
        public string MeasureAdditionalRemarksEn {get; set;}
        public string MeasureDataAvailableEn {get; set;}
        public string MeasurePopulationGroupEn {get; set;}
        public string MeasureSourceShortEn { get; set; }
        public string MeasureSourceLongEn { get; set; }
        public string MeasureUnitShortEn { get; set; }
        public string MeasureUnitLongEn { get; set; }
        
        public string MeasureNameIndexFr {get; set;}
        public string MeasureNameDataToolFr {get; set;}
        public string MeasureDefinitionFr {get; set;}
        public string MeasureMethodFr {get; set;}
        public string MeasureAdditionalRemarksFr {get; set;}
        public string MeasureDataAvailableFr {get; set;}
        public string MeasureUnitFr {get; set;}
        public string MeasureSourceFr {get; set;}
        public string MeasurePopulationGroupFr {get; set;}
        public Point MeasurePoint => DefaultStrata.Points.FirstOrDefault(p => p.Type == 1);

        public int? DefaultStrataId { get; set; }
        [ForeignKey("DefaultStrataId")]
        public virtual Strata DefaultStrata { get; set; }
    }
}