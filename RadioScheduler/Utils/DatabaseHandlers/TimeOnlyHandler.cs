using System.Data;
using Dapper;

namespace RadioScheduler.Utils.DatabaseHandlers;

public class TimeOnlyHandler : SqlMapper.TypeHandler<TimeOnly> {
	public override void SetValue(IDbDataParameter parameter, TimeOnly value) {
		parameter.Value = (long)value.ToTimeSpan().TotalMinutes;
	}

	public override TimeOnly Parse(object value) {
		return TimeOnly.FromTimeSpan(TimeSpan.FromMinutes(Convert.ToInt64(value)));
	}
}
