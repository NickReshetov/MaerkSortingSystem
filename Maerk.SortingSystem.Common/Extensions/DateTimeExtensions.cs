using System;

namespace Maerk.SortingSystem.Common.Extensions
{
    public static class DateTimeExtensions
    {
        public static long ToUnixTimeSeconds(this DateTime dateTime)
        {
            var epoch = dateTime - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return (long)epoch.TotalSeconds;
        }

        public static DateTime ToDateTime(this long unixTimeStamp)
        {
            var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            var convertedTimeStampToDateTime = dateTime
                .AddSeconds(unixTimeStamp)
                .ToUniversalTime();

            return convertedTimeStampToDateTime;
        }
    } 
}
