namespace RadioScheduler.Models.Auth;

public class UserLogin(Guid userId) {
	public Guid Id { get; set; } = Guid.NewGuid();
	public DateTime LoginTime { get; set; } = DateTime.Now;
	public Guid UserId { get; set; } = userId;
	public User User { get; set; } = null!;

}
