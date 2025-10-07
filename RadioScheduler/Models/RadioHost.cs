using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RadioScheduler.Models;

public class RadioHost {
	public Guid Id { get; set; } = Guid.NewGuid();

	[Column("first_name")]
	[MaxLength(255)]
	public string FirstName { get; set; } = "";

	[Column("last_name")]
	[MaxLength(255)]
	public string LastName { get; set; } = "";

	[Column("is_guest")]
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
