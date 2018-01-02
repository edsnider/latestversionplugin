using System;
using System.Text.RegularExpressions;

namespace Plugin.LatestVersion
{
    internal static class Extensions
    {
        public static string MakeSafeForAppStoreShortLinkUrl(this string value)
        {
            // Reference: https://developer.apple.com/library/content/qa/qa1633/_index.html

            var regex = new Regex(@"[©™®!¡""#$%'()*+,\\\-.\/:;<=>¿?@[\]^_`{|}~]*");

            var safeValue = regex.Replace(value, "")
                                 .Replace(" ", "")
                                 .Replace("&", "and")
                                 .ToLower();
            
            return safeValue;
        }
    }
}
