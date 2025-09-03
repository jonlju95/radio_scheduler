namespace RadioScheduler.Models;

public class Tableau {
	public Guid Id { get; init; } = Guid.NewGuid();
	public DateOnly Date { get; init; }
	public List<Timeslot> Timeslots { get; set; } = [];

	public Tableau() {
	}

	public Tableau(Tableau tableau) {
		this.Id = tableau.Id;
		this.Date = tableau.Date;
		this.Timeslots = tableau.Timeslots;
	}
}
