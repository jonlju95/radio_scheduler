namespace RadioScheduler.Models;

public class RadioHost {
	public Guid Id { get; set; }
	public string FirstName { get; set; } = "";
	public string LastName { get; set; } = "";
	public bool IsGuest { get; set; }

	public RadioHost() {
	}

	public RadioHost(Guid id, string firstName, string lastName, bool isGuest) {
		this.Id = id;
		this.FirstName = firstName;
		this.LastName = lastName;
		this.IsGuest = isGuest;
	}

	public RadioHost(RadioHost other) {
		this.Id = other.Id;
		this.FirstName = other.FirstName;
		this.LastName = other.LastName;
		this.IsGuest = other.IsGuest;
	}
}
