using System.Data;
using Dapper;

namespace RadioScheduler.Utils.DatabaseHandlers;

public class UnixMsDateTimeHandler : SqlMapper.TypeHandler<DateTime> {
	public override void SetValue(IDbDataParameter parameter, DateTime value) {
		parameter.Value = new DateTimeOffset(value).ToUnixTimeMilliseconds();
	}

	public override DateTime Parse(object value) {
		return DateTimeOffset.FromUnixTimeMilliseconds((long)value).UtcDateTime;
	}
}
