namespace RadioScheduler.Models;

public class Timeslot {
	public Guid Id { get; init; } = Guid.NewGuid();
	public DateTime StartTime { get; set; }
	public DateTime EndTime { get; set; }
	public Guid TableauId { get; init; }
	public List<RadioHost> RadioHosts { get; set; } = [];
	public RadioShow? RadioShow { get; set; }
	public Studio? Studio { get; set; }

	public Timeslot() {
	}

	public Timeslot(Guid id, DateTime startTime, DateTime endTime, Guid tableauId, List<RadioHost> hosts,
		RadioShow? show, Studio? studio) {
		this.Id = id;
		this.StartTime = startTime;
		this.EndTime = endTime;
		this.TableauId = tableauId;
		this.RadioHosts = hosts;
		this.RadioShow = show;
		this.Studio = studio;
	}
}
