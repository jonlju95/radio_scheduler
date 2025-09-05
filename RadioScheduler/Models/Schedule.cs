using System.ComponentModel.DataAnnotations.Schema;

namespace RadioScheduler.Models;

public class Schedule {
	public Guid Id { get; set; }
	public DateOnly StartDate { get; set; }
	public DateOnly EndDate { get; set; }
	public ICollection<Tableau> Tableaux { get; set; } = new List<Tableau>();

	public Schedule() {
	}

	public Schedule(Guid id, DateOnly startDate, DateOnly endDate) {
		this.Id = id;
		this.StartDate = startDate;
		this.EndDate = endDate;
	}

	public Schedule(Schedule other) {
		this.Id = other.Id;
		this.StartDate = other.StartDate;
		this.EndDate = other.EndDate;
		this.Tableaux = other.Tableaux;
	}
}
