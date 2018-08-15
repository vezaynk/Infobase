using System;
using System.Globalization;

namespace Infobase
{
    public static class Formatter {
        public static string Standard(double format, string local) {
            CultureInfo myCI = new CultureInfo(local, false);
            return String.Format(myCI, "{0:0.0}", format);
        }
    }
}