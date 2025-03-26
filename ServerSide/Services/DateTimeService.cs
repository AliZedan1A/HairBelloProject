using System.Runtime.InteropServices;

namespace ServerSide.Services
{
    public class DateTimeService
    {
        private readonly TimeZoneInfo israelTimeZone;
        public DateTimeService()
        {
            string timeZoneId = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
    ? "Israel Standard Time"  // ويندوز
    : "Asia/Jerusalem";        // لينكس، أندرويد، iOS، ماك

            israelTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);

        }
        public DateTime IsraelNow()
        {
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, israelTimeZone);
        }
        public DateTime ConvertUtcToIsrael(DateTime utcDateTime)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, israelTimeZone);
        }

        public DateTime ConvertIsraelToUtc(DateTime israelDateTime)
        {
            return TimeZoneInfo.ConvertTimeToUtc(israelDateTime, israelTimeZone);
        }
    }
}
