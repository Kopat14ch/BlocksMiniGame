using System.Globalization;

namespace Code.Common.Extensions
{
    public static class FloatExtension
    {
        public static string Format(this float value) => 
            value.ToString("F1", CultureInfo.InvariantCulture);
    }
}