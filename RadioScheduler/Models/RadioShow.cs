namespace RadioScheduler.Models;

public class RadioShow {
	public Guid Id { get; set; }
	public string Name { get; set; } = "";
	public int DurationMinutes { get; set; }

	public RadioShow() {
	}

	public RadioShow(Guid id, string name, int durationMinutes) {
		this.Id = id;
		this.Name = name;
		this.DurationMinutes = durationMinutes;
	}

	public RadioShow(RadioShow other) {
		this.Id = other.Id;
		this.Name = other.Name;
		this.DurationMinutes = other.DurationMinutes;
	}
}
