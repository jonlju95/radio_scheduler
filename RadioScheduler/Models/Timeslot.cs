namespace RadioScheduler.Models;

public class Timeslot {
	public Guid Id { get; init; }
	public TimeOnly StartTime { get; set; }
	public TimeOnly EndTime { get; set; }
	public List<RadioHost> Hosts { get; set; } = [];
	public RadioShow? Show { get; set; }
	public Studio? Studio { get; set; }

	public Timeslot() {
	}

	public Timeslot(Guid id, TimeOnly startTime, TimeOnly endTime, List<RadioHost> hosts, RadioShow? show, Studio? studio) {
		this.Id = id;
		this.StartTime = startTime;
		this.EndTime = endTime;
		this.Hosts = hosts;
		this.Show = show;
		this.Studio = studio;
	}

	public Timeslot(Timeslot other) {
		this.Id = other.Id;
		this.StartTime = other.StartTime;
		this.EndTime = other.EndTime;
		this.Hosts = other.Hosts;
		this.Show = other.Show;
		this.Studio = other.Studio;
	}
}
