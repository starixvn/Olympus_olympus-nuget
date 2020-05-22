using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Olympus.HttpLogger
{
    public class RegexUtility
    {
        private static List<Regex> PasswordRegexes = new List<Regex>();

        static RegexUtility()
        {
            PasswordRegexes.Add(new Regex("\\s*userpassword\\s*=[^=&]+", RegexOptions.IgnoreCase));
            PasswordRegexes.Add(new Regex("\"?\\s*userpassword\\s*\"?\\s*:\\s*\"?[^,\\\"]+\"?", RegexOptions.IgnoreCase));
            PasswordRegexes.Add(new Regex("\"?\\s*password\\s*\"?\\s*:\\s*\"?[^,\\\"]+\"?", RegexOptions.IgnoreCase));
            PasswordRegexes.Add(new Regex("\\s*password\\s*=[^=&]+", RegexOptions.IgnoreCase));
        }

        public static string ReplaceSensitiveText(string text)
        {
            foreach (var regex in PasswordRegexes)
            {
                text = regex.Replace(text, "**** sensitive password ****");
            }

            return text;
        }
    }
}
