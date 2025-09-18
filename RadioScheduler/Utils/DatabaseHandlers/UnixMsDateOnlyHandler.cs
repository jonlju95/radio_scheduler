using System.Data;
using Dapper;

namespace RadioScheduler.Utils.DatabaseHandlers;

public class UnixMsDateOnlyHandler : SqlMapper.TypeHandler<DateOnly> {
	public override void SetValue(IDbDataParameter parameter, DateOnly value) {
		DateTime utcDateTime = value.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc);
		parameter.Value = new DateTimeOffset(utcDateTime).ToUnixTimeMilliseconds();
	}

	public override DateOnly Parse(object value) {
		DateTime utcDateTime = DateTimeOffset.FromUnixTimeMilliseconds((long)value).UtcDateTime;
		return DateOnly.FromDateTime(utcDateTime);
	}
}
