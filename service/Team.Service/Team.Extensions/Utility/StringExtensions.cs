namespace Team.Extensions.Utility
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        public static string GetValueOrDefault(this string s, string defaultValue)
        {
            return string.IsNullOrEmpty(s) ? defaultValue : s;
        }

        public static string EnsureTrailingSlash(this string s)
        {
            return s.StartsWith('/') ? s : $"/{s}";
        }

        public static string EnsureLeadingSlash(this string s)
        {
            return s.EndsWith('/') ? s : $"{s}/";
        }
    }
}