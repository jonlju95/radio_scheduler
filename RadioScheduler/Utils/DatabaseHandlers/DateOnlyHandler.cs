using System.Data;
using Dapper;

namespace RadioScheduler.Utils.DatabaseHandlers;

public class DateOnlyHandler : SqlMapper.TypeHandler<DateOnly> {
	public override void SetValue(IDbDataParameter parameter, DateOnly value) {
		parameter.Value = value.Year * 10000 + value.Month * 100 + value.Day;
	}

	public override DateOnly Parse(object value) {
		long dbValue = Convert.ToInt64(value);
		int year = (int)(dbValue / 10000);
		int month = (int)(dbValue / 100) % 100;
		int day = (int)(dbValue % 100);

		return new DateOnly(year, month, day);
	}
}
