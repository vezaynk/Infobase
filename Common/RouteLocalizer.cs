using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;

namespace Infobase.Common
{
    public static class RouteLocalizer
    {
        private static Dictionary<(string, string), string> dict = new Dictionary<(string, string), string>();
        static RouteLocalizer() {
            
            // Add Index
            dict.Add(("FR", "index"), "index");

            // Add measure details
            dict.Add(("FR", "détails"), "details");

            // Add measure details
            dict.Add(("FR", "outil-de-donnees"), "datatool");

            // Add strata controller
            dict.Add(("FR", "stratafr"), "strata");
			
			// Add language controller
            dict.Add(("FR", "fr-ca"), "en-ca");
        }
        public static string LocalizeRouteElement(string culture, string part) {
        try
            {
                return dict[(culture.ToUpper(), part.ToLower())];
            }
            catch (System.Exception)
            {
              return part;
            }
        }

        public static string ReverseLookup(string culture, string part) {
            return  CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dict.Where(kvp => kvp.Key.Item1 == culture && kvp.Value == part).Select(kvp => kvp.Key.Item2).FirstOrDefault() ?? part);
            
        }
    }
    
}