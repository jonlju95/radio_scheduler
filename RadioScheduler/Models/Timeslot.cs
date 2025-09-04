namespace RadioScheduler.Models;

public class Timeslot {
	public Guid Id { get; init; } = Guid.NewGuid();
	public TimeOnly Start { get; set; }
	public TimeOnly End { get; set; }
	public List<RadioHost> Hosts { get; set; } = [];
	public RadioShow? Show { get; set; }
	public Studio? Studio { get; set; }

	public Timeslot() {
	}

	public Timeslot(TimeOnly start, TimeOnly end, List<RadioHost> hosts, RadioShow? show, Studio? studio) {
		this.Start = start;
		this.End = end;
		this.Hosts = hosts;
		this.Show = show;
		this.Studio = studio;
	}

	public Timeslot(Timeslot other) {
		this.Id = other.Id;
		this.Start = other.Start;
		this.End = other.End;
		this.Hosts = other.Hosts;
		this.Show = other.Show;
		this.Studio = other.Studio;
	}
}
