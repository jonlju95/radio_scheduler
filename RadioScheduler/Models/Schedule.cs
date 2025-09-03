using System.Text.Json.Serialization;

namespace RadioScheduler.Models;

public class Schedule {
	public Guid Id { get; init; }
	public DateOnly StartDate { get; set; }
	public DateOnly EndDate { get; set; }
	public List<Tableau> Tableaux { get; set; } = [];

	public Schedule() {
	}

	public Schedule(Guid id, DateOnly startDate, DateOnly endDate) {
		this.Id = id;
		this.StartDate = startDate;
		this.EndDate = endDate;
		this.Tableaux = [];
	}

	public Schedule(Schedule other) {
		this.Id = other.Id;
		this.StartDate = other.StartDate;
		this.EndDate = other.EndDate;
		this.Tableaux = other.Tableaux;
	}
}
