namespace RadioScheduler.Models;

public class Tableau {
	public Guid Id { get; set; } = Guid.NewGuid();
	public DateOnly Date { get; set; }
	public ICollection<Timeslot> Timeslots { get; set; } = new List<Timeslot>();

	public Tableau() {
	}

	public Tableau(Guid id, DateOnly date) {
		this.Id = id;
		this.Date = date;
	}
}
