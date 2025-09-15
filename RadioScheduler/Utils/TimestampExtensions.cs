using RadioScheduler.Models.Base;

namespace RadioScheduler.Utils;

public static class TimestampExtensions {
	private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
	public static double ToUnixS(this Timestamp timestamp) => timestamp.UnixTimeMs / 1000.0;
	public static double ToUnixMins(this Timestamp timestamp) => timestamp.UnixTimeMs / 60000.0;
	public static DateTime ToDateTimeUTC(this Timestamp timestamp) => UnixEpoch.AddMilliseconds(timestamp.UnixTimeMs).ToUniversalTime();
	public static string ToLabel(this Timestamp timestamp) => timestamp.ToDateTimeUTC().ToString("yyyy-MM-dd HH:mm:ss tt");

	public static bool IsWithinRange(this Timestamp timestamp, Timestamp startTime, Timestamp endTime) {
		if (timestamp == null || startTime == null || endTime == null) {
			return false;
		}

		return timestamp.UnixTimeMs >= startTime.UnixTimeMs && timestamp.UnixTimeMs <= endTime.UnixTimeMs;
	}
}
