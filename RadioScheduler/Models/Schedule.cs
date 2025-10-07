namespace RadioScheduler.Models;

public class Schedule {
	public Guid Id { get; set; } = Guid.NewGuid();
	public int Year { get; set; } = DateTime.Now.Year;
	public int Month { get; set; } = DateTime.Now.Month;
	public ICollection<Guid?> TableauIds { get; set; } = new List<Guid?>();

	public Schedule() {
	}

	public Schedule(Guid id, int year, int month) {
		this.Id = id;
		this.Year = year;
		this.Month = month;
	}
}
