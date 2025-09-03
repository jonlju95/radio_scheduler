namespace RadioScheduler.Models;

public class Schedule {
	public Guid Id { get; init; } = Guid.NewGuid();
	public int WeekNumber { get; set; }
	public DateOnly StartDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
	public DateOnly EndDate { get; set; } = DateOnly.FromDateTime(DateTime.Now).AddDays(7);
	public List<Tableau> Tableaux { get; set; } = new List<Tableau>(7);

	public Schedule() {
	}

	public Schedule(Schedule other) {
		Id = other.Id;
		WeekNumber = other.WeekNumber;
		StartDate = other.StartDate;
		EndDate = other.EndDate;
		Tableaux = other.Tableaux;
	}
}
