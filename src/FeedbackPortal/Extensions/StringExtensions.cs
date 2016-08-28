using System.Text.RegularExpressions;

namespace FeedbackPortal.Extensions
{
    public static class StringExtensions
    {
        public static string Clean(this string input)
        {
            var regex = new Regex("[^a-z0-9\\-_]", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);

            input = input.Replace(" ", "-");
            var cleaned = regex.Replace(input, "").ToLower();

            while (cleaned.Contains("--"))
            {
                cleaned = cleaned.Replace("--", "-");
            }

            return cleaned;
        }

        public static string Truncate(this string input, int max, string append = "")
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            if (input.Length <= max)
                return input;
            
            var result = input.Substring(0, max);

            if (!string.IsNullOrEmpty(append))
                result = result + append;

            return result;
        }
    }
}
