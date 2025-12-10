using RadioScheduler.Utils.Enum;

namespace RadioScheduler.Models.Auth;

public class Role {
	public Guid Id { get; set; } = Guid.NewGuid();
	public RoleEnum? RoleName { get; set; }
	public List<UserRole> UserRoles { get; } = [];
}
