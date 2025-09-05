using System.Data;
using Dapper;

namespace RadioScheduler.Utils.DatabaseHandlers;

public class GuidHandler : SqlMapper.TypeHandler<Guid> {
	public override void SetValue(IDbDataParameter parameter, Guid value) {
		parameter.Value = value.ToString();
	}

	public override Guid Parse(object value) {
		return Guid.Parse(value?.ToString() ?? Guid.Empty.ToString());
	}
}
