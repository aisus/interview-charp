using System.Globalization;

namespace Team.Extensions.Utility
{
    public static class FloatExtensions
    {
        public static string ToInvariantString(this float f)
        {
            return f.ToString(CultureInfo.InvariantCulture);
        }
    }
}