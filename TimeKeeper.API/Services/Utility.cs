using System;

namespace TimeKeeper.API.Services
{
    public static class Utility
    {
        public static string ToHex(this string v)
        {
            return Convert.ToByte(v).ToString("x2");
        }

        public static bool Weekend(this DateTime date)
        {
            return (date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday);
        }
    }
}
