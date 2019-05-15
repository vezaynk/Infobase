using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Infobase.Models.PASS
{
    public class Strata
    {
        public int StrataId { get; set; }
        public int MeasureId { get; set; }
        public int Index {get;set;}
        public virtual Measure Measure { get; set; }
        [InverseProperty("Strata")]
        public virtual ICollection<Point> Points { get; set; }
        /* Text getters */

        public string StrataNotes(string culture) {
            switch (culture) {
                case "en-ca": return StrataNotesEn;
                case "fr-ca": return StrataNotesFr;
            }
            return "No culture";
        }

        public string StrataName(string culture) {
            switch (culture) {
                case "en-ca": return StrataNameEn;
                case "fr-ca": return StrataNameFr;
            }
            return "No culture";
        }

        public string StrataPopulationTitleFragment(string culture) {
            switch (culture) {
                case "en-ca": return StrataPopulationTitleFragmentEn;
                case "fr-ca": return StrataPopulationTitleFragmentFr;
            }
            return "No culture";
        }

        public string StrataSource(string culture) {
            switch (culture) {
                case "en-ca": return StrataSourceEn;
                case "fr-ca": return StrataSourceFr;
            }
            return "No culture";
        }
        public string StrataNotesEn { get; set; }
        public string StrataNameEn { get; set; }
        public string StrataSourceEn { get; set; }
        public string StrataPopulationTitleFragmentEn { get; set; }
        
        public string StrataNotesFr { get; set; }
        public string StrataNameFr { get; set; }
        public string StrataSourceFr { get; set; }
        public string StrataPopulationTitleFragmentFr { get; set; }
    }
}
