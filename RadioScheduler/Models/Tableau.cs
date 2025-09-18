namespace RadioScheduler.Models;

public class Tableau {
	public Guid Id { get; init; } = Guid.NewGuid();
	public DateOnly Date { get; init; }
	public Guid ScheduleId { get; init; }
	public List<Timeslot> Timeslots { get; set; } = [];

	public Tableau() {
	}

	public Tableau(Guid id, DateOnly date, Guid scheduleId) {
		this.Id = id;
		this.Date = date;
		this.ScheduleId = scheduleId;
	}
}
