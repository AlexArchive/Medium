using System;

namespace Blog.Helpers
{
    public static class DateTimeExtensions
    {
        public static string Humanize(this DateTime theDate)
        {
            var timeSinceDate = new TimeSpan(DateTime.Now.Ticks - theDate.Ticks);
            var delta = Math.Abs(timeSinceDate.TotalSeconds);

            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            if (delta < 0)
            {
                return "not yet";
            }
            if (delta < 1 * MINUTE)
            {
                return timeSinceDate.Seconds == 1 ? "one second ago" : timeSinceDate.Seconds + " seconds ago";
            }
            if (delta < 2 * MINUTE)
            {
                return "a minute ago";
            }
            if (delta < 45 * MINUTE)
            {
                return timeSinceDate.Minutes + " minutes ago";
            }
            if (delta < 90 * MINUTE)
            {
                return "an hour ago";
            }
            if (delta < 24 * HOUR)
            {
                return timeSinceDate.Hours + " hours ago";
            }
            if (delta < 48 * HOUR)
            {
                return "yesterday";
            }
            if (delta < 30 * DAY)
            {
                return timeSinceDate.Days + " days ago";
            }
            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)timeSinceDate.Days / 30));
                return months <= 1 ? "one month ago" : months + " months ago";
            }
            int years = Convert.ToInt32(Math.Floor((double)timeSinceDate.Days / 365));
            return years <= 1 ? "one year ago" : years + " years ago";
        }
    }
}