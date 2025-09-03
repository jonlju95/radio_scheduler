namespace RadioScheduler.Models;

public class Tableau {
	public Guid Id { get; init; } = Guid.NewGuid();
	public DateOnly Date { get; init; }
	public List<Timeslot> Timeslots { get; set; } = [];

	public Tableau() {
	}

	public Tableau(Guid id, DateOnly date) {
		this.Id = id;
		this.Date = date;
		this.Timeslots = [];
	}

	public Tableau(Tableau tableau) {
		this.Id = tableau.Id;
		this.Date = tableau.Date;
		this.Timeslots = tableau.Timeslots;
	}
}
