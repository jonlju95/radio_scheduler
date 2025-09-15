using RadioScheduler.Models.Base;

namespace RadioScheduler.Models;

public class Timeslot {
	public Guid Id { get; init; }
	public Timestamp StartTime { get; set; } = new Timestamp();
	public Timestamp EndTime { get; set; } = new Timestamp();
	public Guid TableauId { get; init; }
	public List<Guid> HostIds { get; set; } = [];
	public Guid? ShowId { get; set; }
	public Guid? StudioId { get; set; }

	public Timeslot() {
	}

	public Timeslot(Guid id, Timestamp startTime, Timestamp endTime, Guid tableauId, List<Guid> hostIds, Guid? showId,
		Guid? studioId) {
		this.Id = id;
		this.StartTime = startTime;
		this.EndTime = endTime;
		this.TableauId = tableauId;
		this.HostIds = hostIds;
		this.ShowId = showId;
		this.StudioId = studioId;
	}
}
