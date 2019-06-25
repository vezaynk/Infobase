using System;
using System.Globalization;

namespace Infobase
{
    public static class Formatter {
        public static string Standard(double format, string units, string languageCode) {
            string formattedNumber = String.Format(new CultureInfo(languageCode, false), "{0:0.0}", format);
            if (units == null)
                return formattedNumber;

            bool spacing = true;
            if (languageCode == "en-ca" && units == "%")
                spacing = false;

            return $"{formattedNumber}{(spacing ? " " : "")}{units}";
        }
    }
}