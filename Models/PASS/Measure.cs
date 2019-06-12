using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Infobase.Automation;

namespace Infobase.Models.PASS
{
    [TextData("NameIndex")]
    [TextData("NameDataTool")]
    [TextData("AdditionalRemarks")]
    [TextData("DataAvailable")]
    [TextData("Definition")]
    [TextData("Method")]
    [TextData("PopulationGroup")]
    [TextData("SourceLong")]
    [TextData("SourceShort")]
    [TextData("UnitLong")]
    [TextData("UnitShort")]
    [ChildOf(typeof(Indicator))]
    [ParentOf(typeof(Strata), true)]
    [Modifier(ModelModifier.Aggregator | ModelModifier.CVBoundries | ModelModifier.Include)]
    public class Measure
    {
        public int MeasureId { get; set; }
        public int IndicatorId { get; set; }
        public int Index { get; set; }
        public virtual Indicator Indicator { get; set; }
        public bool Included { get; set; }
        public bool Aggregator { get; set; }
        public double? CVWarnAt { get; set; }
        public double? CVSuppressAt { get; set; }

        [InverseProperty("Measure")]
        public virtual ICollection<Strata> Stratas { get; set; }

        public string MeasureNameIndex(string culture) {
            switch (culture) {
                case "en-ca": return MeasureNameIndexEn;
                case "fr-ca": return MeasureNameIndexFr;
            }
            return "No culture";
        }
        public string MeasureNameDataTool(string culture) {
            switch (culture) {
                case "en-ca": return MeasureNameDataToolEn;
                case "fr-ca": return MeasureNameDataToolFr;
            }
            return "No culture";
        }
        public string MeasureDefinition(string culture) {
            switch (culture) {
                case "en-ca": return MeasureDefinitionEn;
                case "fr-ca": return MeasureDefinitionFr;
            }
            return "No culture";
        }
        public string MeasureMethod(string culture) {
            switch (culture) {
                case "en-ca": return MeasureMethodEn;
                case "fr-ca": return MeasureMethodFr;
            }
            return "No culture";
        }
        public string MeasureAdditionalRemarks(string culture) {
            switch (culture) {
                case "en-ca": return MeasureAdditionalRemarksEn;
                case "fr-ca": return MeasureAdditionalRemarksFr;
            }
            return "No culture";
        }
        public string MeasureDataAvailable(string culture) {
            switch (culture) {
                case "en-ca": return MeasureDataAvailableEn;
                case "fr-ca": return MeasureDataAvailableFr;
            }
            return "No culture";
        }
        public string MeasurePopulationGroup(string culture) {
            switch (culture) {
                case "en-ca": return MeasurePopulationGroupEn;
                case "fr-ca": return MeasurePopulationGroupFr;
            }
            return "No culture";
        }
        public string MeasureSourceLong(string culture) {
            switch (culture) {
                case "en-ca": return MeasureSourceLongEn;
                case "fr-ca": return MeasureSourceLongFr;
            }
            return "No culture";
        }
        public string MeasureSourceShort(string culture) {
            switch (culture) {
                case "en-ca": return MeasureSourceShortEn;
                case "fr-ca": return MeasureSourceShortFr;
            }
            return "No culture";
        }
        public string MeasureUnitLong(string culture) {
            switch (culture) {
                case "en-ca": return MeasureUnitLongEn;
                case "fr-ca": return MeasureUnitLongFr;
            }
            return "No culture";
        }
        public string MeasureUnitShort(string culture) {
            switch (culture) {
                case "en-ca": return MeasureUnitShortEn;
                case "fr-ca": return MeasureUnitShortFr;
            }
            return "No culture";
        }
        public string MeasureNameIndexEn { get; set; }
        
        public string MeasureNameDataToolEn { get; set; }
        public string MeasureDefinitionEn { get; set; }
        public string MeasureMethodEn { get; set; }
        public string MeasureAdditionalRemarksEn { get; set; }
        public string MeasureDataAvailableEn { get; set; }
        public string MeasurePopulationGroupEn { get; set; }
        public string MeasureSourceShortEn { get; set; }
        public string MeasureSourceLongEn { get; set; }
        public string MeasureUnitShortEn { get; set; }
        public string MeasureUnitLongEn { get; set; }


        public string MeasureNameIndexFr { get; set; }
        public string MeasureNameDataToolFr { get; set; }
        public string MeasureDefinitionFr { get; set; }
        public string MeasureMethodFr { get; set; }
        public string MeasureAdditionalRemarksFr { get; set; }
        public string MeasureDataAvailableFr { get; set; }
        public string MeasurePopulationGroupFr { get; set; }
        public string MeasureSourceShortFr { get; set; }
        public string MeasureSourceLongFr { get; set; }
        public string MeasureUnitShortFr { get; set; }
        public string MeasureUnitLongFr { get; set; }

        public int? DefaultStrataId { get; set; }
        [ForeignKey("DefaultStrataId")]
        public virtual Strata DefaultStrata { get; set; }
    }
}