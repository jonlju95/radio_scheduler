namespace RadioScheduler.Models;

public class Tableau {
	public Guid Id { get; init; } = Guid.NewGuid();
	public DateOnly Date { get; init; }
	public Guid ScheduleId { get; init; }
	public List<Guid> TimeslotIds { get; set; } = [];

	public Tableau() {
	}

	public Tableau(Guid id, DateOnly date, Guid scheduleId) {
		this.Id = id;
		this.Date = date;
		this.ScheduleId = scheduleId;
	}

	public Tableau(Tableau tableau) {
		this.Id = tableau.Id;
		this.Date = tableau.Date;
		this.ScheduleId = tableau.ScheduleId;
		this.TimeslotIds = tableau.TimeslotIds;
	}
}
