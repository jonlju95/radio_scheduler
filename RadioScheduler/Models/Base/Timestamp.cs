namespace RadioScheduler.Models.Base;

public class Timestamp {
	public double UnixTimeMs { get; set; } = 0.0;
	private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

	public static Timestamp CurrentTimestamp() {
		return FromDateTimeUTC(DateTime.UtcNow);
	}

	public static Timestamp FromUnixTimeSec(double unixS) {
		return new Timestamp {
			UnixTimeMs = unixS * 1000.0
		};
	}

	public static Timestamp FromUnixTimeMilliSec(double unixMs) {
		return new Timestamp {
			UnixTimeMs = unixMs
		};
	}

	public static Timestamp FromDateTimeUTC(DateTime dateTime) {
		if (dateTime.Kind == DateTimeKind.Unspecified) {
			dateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
		}

		return new Timestamp {
			UnixTimeMs = (dateTime - UnixEpoch).TotalMilliseconds
		};
	}

	public static Timestamp FromTimestampUTC(string timestampUTC) {
		bool tryDateTime = DateTime.TryParse(timestampUTC, out DateTime res);
		if (!tryDateTime) {
			return new Timestamp();
		}

		long unixMs = new DateTimeOffset(res.ToUniversalTime()).ToUnixTimeMilliseconds();
		return new Timestamp() {
			UnixTimeMs = unixMs,
		};
	}

	public override bool Equals(object? obj) {
		return obj is Timestamp timestamp && this.UnixTimeMs.Equals(timestamp.UnixTimeMs);
	}

	public override int GetHashCode() {
		return -262018729 + this.UnixTimeMs.GetHashCode();
	}

	public static bool operator ==(Timestamp left, Timestamp? right) {
		return EqualityComparer<Timestamp>.Default.Equals(left, right);
	}

	public static bool operator !=(Timestamp left, Timestamp right) {
		return !(left == right);
	}

	public static bool operator >(Timestamp left, Timestamp right) {
		return left.UnixTimeMs > right.UnixTimeMs;
	}

	public static bool operator <(Timestamp left, Timestamp right) {
		return left.UnixTimeMs < right.UnixTimeMs;
	}

	public static bool operator >=(Timestamp left, Timestamp right) {
		return left.UnixTimeMs >= right.UnixTimeMs;
	}

	public static bool operator <=(Timestamp left, Timestamp right) {
		return left.UnixTimeMs <= right.UnixTimeMs;
	}
}
