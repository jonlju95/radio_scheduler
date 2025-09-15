using System.Data;
using Dapper;
using RadioScheduler.Models.Base;

namespace RadioScheduler.Utils.DatabaseHandlers;

public class TimestampHandler : SqlMapper.TypeHandler<Timestamp> {
	public override void SetValue(IDbDataParameter parameter, Timestamp? value) {
		parameter.Value = value?.UnixTimeMs;
	}

	public override Timestamp Parse(object value) {
		if (!double.TryParse(value.ToString(), out double timestamp)) {
			return Timestamp.CurrentTimestamp();
		}

		return new Timestamp {
			UnixTimeMs = timestamp
		};
	}
}
