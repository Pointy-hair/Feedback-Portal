using System;

namespace FeedbackPortal.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToRelativeTimeTag(this DateTime dt)
        {
            return $"<time title=\"{dt:g}\">{dt.ToRelativeTime()}</time>";
        }

        public static string ToRelativeTime(this DateTime dt)
        {
            if (dt <= DateTime.UtcNow)
                return ToPastRelativeTime(dt, DateTime.UtcNow);

            // Future relative time not yet supported
            return dt.ToString("g");
        }

        private static string ToPastRelativeTime(DateTime dt, DateTime utc)
        {
            var ts = utc - dt;
            var delta = ts.TotalSeconds;

            if (delta < 60)
                return ts.Seconds == 1 ? "1 sec ago" : ts.Seconds + " secs ago";
            if (delta < 3600) // 60 mins * 60 secs
                return ts.Minutes == 1 ? "1 min ago" : ts.Minutes + " mins ago";
            if (delta < 86400) // 24 hrs * 60 mins * 60 secs
                return ts.Hours == 1 ? "1 hr ago" : ts.Hours + " hrs ago";

            var days = ts.Days;
            if (days == 1)
                return "yesterday";
            if (days <= 2)
                return 2 + " days ago";
            if (utc.Year == dt.Year)
                return dt.ToString("MMM %d 'at' %H:mmm tt");

            return dt.ToString(@"MMM %d \'yy 'at' %H:mmm tt");
        }
    }
}
