using System;

namespace Up.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToClearDate(this DateTime value)
        {
            return value.ToString("dd/MM/yyyy");
        }

        public static string ToEditionDate(this DateTime value)
        {
            return value.ToString("yyyy-MM-dd");
        }
    }
}
