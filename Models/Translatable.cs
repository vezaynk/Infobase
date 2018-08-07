using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infobase.Models
{
    public class Translatable: Dictionary<(string, string), string> {
        public Translatable(IDictionary<(string, string), string> dict): base(dict) {

        }

        public Translatable() {
            
        }
        public string Get((string, string) key) {
            string value;
            var (language, type) = key;
            if (type == null) {
                // Just take the first match fam
                return this.ToLookup(i => i.Key.Item1)[language].FirstOrDefault().Value;
            }
            return this.TryGetValue(key, out value) ? value : null;
        }
    }
}