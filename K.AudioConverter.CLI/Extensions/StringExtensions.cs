// Ignore Spelling: Cli

namespace K.AudioConverter.Cli.Extensions
{
    public static class StringExtensions
    {
        public static string EnsurePeriodPrefix(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return string.Empty;
            }

            // Trim any leading periods and pre-pend a single period
            return "." + input.TrimStart('.').Trim();
        }
    }
}
