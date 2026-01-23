using RadioScheduler.Utils.Enum;

namespace RadioScheduler.Models.Auth;

public class Role {
	public Guid Id { get; set; } = Guid.NewGuid();
	public RoleEnum? Code { get; set; }
	public string Title { get; set; } = "";
}
