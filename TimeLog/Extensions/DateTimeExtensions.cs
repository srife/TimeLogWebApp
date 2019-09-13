using System;

namespace TimeLog.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime RoundUp(DateTime dt, TimeSpan d)
        {
            return new DateTime((dt.Ticks + d.Ticks - 1) / d.Ticks * d.Ticks, dt.Kind);
        }

        public static DateTimeOffset RoundUp2(DateTimeOffset dto, TimeSpan d)
        {
            return new DateTimeOffset(new DateTime((dto.DateTime.Ticks + d.Ticks - 1) / d.Ticks * d.Ticks, dto.DateTime.Kind));
        }
    }
}