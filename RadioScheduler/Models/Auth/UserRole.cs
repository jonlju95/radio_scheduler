using System.Text.Json.Serialization;

namespace RadioScheduler.Models.Auth;

public class UserRole {
	[JsonIgnore]
	public Guid UserId { get; set; }
	[JsonIgnore]
	public User User { get; set; } = null!;

	[JsonIgnore]
	public Guid RoleId { get; set; }
	public Role Role { get; set; } = null!;
}
