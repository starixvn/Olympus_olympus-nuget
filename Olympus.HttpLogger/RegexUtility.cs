namespace Olympus.HttpLogger
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public class RegexUtility
    {
        private static readonly List<Regex> PasswordRegex = new List<Regex>();

        static RegexUtility()
        {
            PasswordRegex.Add(new Regex("\\s*userpassword\\s*=[^=&]+", RegexOptions.IgnoreCase));
            PasswordRegex.Add(new Regex("\"?\\s*userpassword\\s*\"?\\s*:\\s*\"?[^,\\\"]+\"?", RegexOptions.IgnoreCase));
            PasswordRegex.Add(new Regex("\"?\\s*password\\s*\"?\\s*:\\s*\"?[^,\\\"]+\"?", RegexOptions.IgnoreCase));
            PasswordRegex.Add(new Regex("\\s*password\\s*=[^=&]+", RegexOptions.IgnoreCase));
        }

        public static string ReplaceSensitiveText(string text)
        {
            foreach (var regex in PasswordRegex)
            {
                text = regex.Replace(text, "**** sensitive password ****");
            }

            return text;
        }
    }
}
