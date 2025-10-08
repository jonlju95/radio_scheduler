using System.ComponentModel.DataAnnotations;

namespace RadioScheduler.Models;

public class RadioShow {
	public Guid Id { get; set; } = Guid.NewGuid();
	[MaxLength(255)] public string Title { get; set; } = "";
	public int DurationMin { get; set; }

	public RadioShow() {
	}

	public RadioShow(Guid id, string title, int durationMin) {
		this.Id = id;
		this.Title = title;
		this.DurationMin = durationMin;
	}

	public RadioShow(RadioShow other) {
		this.Id = other.Id;
		this.Title = other.Title;
		this.DurationMin = other.DurationMin;
	}
}
