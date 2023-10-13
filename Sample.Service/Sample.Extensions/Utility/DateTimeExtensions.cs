using System;

namespace Sample.Extensions.Utility
{
    public static class DateTimeExtensions
    {
        public static DateTime EndOfDay(this DateTime dt)
        {
            return new DateTime(
                dt.Year,
                dt.Month,
                dt.Day,
                23,
                59,
                59,
                DateTimeKind.Local);
        }

        public static DateTime GetTime(this DateTime dt)
        {
            return new DateTime(1, 1, 1, dt.Hour, dt.Minute, dt.Second, DateTimeKind.Local);
        }

        public static DateTime SetTime(this DateTime dt, TimeSpan time)
        {
            return new DateTime(
                dt.Year,
                dt.Month,
                dt.Day,
                time.Hours,
                time.Minutes,
                time.Seconds,
                DateTimeKind.Local);
        }

        public static DateTime SetDate(this DateTime dt, DateTime date)
        {
            return new DateTime(
                date.Year,
                date.Month,
                date.Day,
                dt.Hour,
                dt.Minute,
                dt.Second,
                DateTimeKind.Local);
        }

        public static string ToISOLocal(this DateTime dt)
        {
            return DateTime.SpecifyKind(dt, DateTimeKind.Local).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'sszzz");
        }

        public static string ToShortTimeString(this DateTime? dt)
        {
            return dt.HasValue ? dt.Value.ToShortTimeString() : null;
        }
    }
}