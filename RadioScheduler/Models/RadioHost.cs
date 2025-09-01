namespace RadioScheduler.Models;

public class RadioHost {
	public Guid Id { get; set; }
	public string FirstName { get; set; } = "";
	public string LastName { get; set; } = "";
	public bool IsGuest { get; set; }
}
