namespace RadioScheduler.Models;

public class RadioShow {
	public Guid Id { get; } = Guid.NewGuid();
	public string Name { get; set; } = "";
	public int DurationMinutes { get; set; }
}
