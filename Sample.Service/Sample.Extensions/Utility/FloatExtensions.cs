using System.Globalization;

namespace Sample.Extensions.Utility
{
    public static class FloatExtensions
    {
        public static string ToInvariantString(this float f)
        {
            return f.ToString(CultureInfo.InvariantCulture);
        }
    }
}