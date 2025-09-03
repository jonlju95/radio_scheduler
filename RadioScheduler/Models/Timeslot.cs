namespace RadioScheduler.Models;

public class Timeslot {
	public Guid Id { get; init; } = Guid.NewGuid();
	public DateTime Start { get; set; }
	public DateTime End { get; set; }
	public List<Guid> HostIds { get; set; } = [];
	public Guid ShowId { get; set; }
	public Guid StudioId { get; set; }

	public Timeslot() {
	}

	public Timeslot(Timeslot other) {
		this.Id = other.Id;
		this.Start = other.Start;
		this.End = other.End;
		this.HostIds = other.HostIds;
		this.ShowId = other.ShowId;
		this.StudioId = other.StudioId;
	}
}
