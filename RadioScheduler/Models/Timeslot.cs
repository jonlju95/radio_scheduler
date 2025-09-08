namespace RadioScheduler.Models;

public class Timeslot {
	public Guid Id { get; init; }
	public TimeOnly StartTime { get; set; }
	public TimeOnly EndTime { get; set; }
	public Guid TableauId { get; init; }
	public List<Guid> HostIds { get; set; } = [];
	public Guid? ShowId { get; set; }
	public Guid? StudioId { get; set; }

	public Timeslot() {
	}

	public Timeslot(Guid id, TimeOnly startTime, TimeOnly endTime, List<Guid> hostIds, Guid? showId, Guid? studioId) {
		this.Id = id;
		this.StartTime = startTime;
		this.EndTime = endTime;
		this.HostIds = hostIds;
		this.ShowId = showId;
		this.StudioId = studioId;
	}

	public Timeslot(Timeslot other) {
		this.Id = other.Id;
		this.StartTime = other.StartTime;
		this.EndTime = other.EndTime;
		this.HostIds = other.HostIds;
		this.ShowId = other.ShowId;
		this.StudioId = other.StudioId;
	}
}
