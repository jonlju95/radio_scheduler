namespace RadioScheduler.Models;

public class RadioTimeslot {
	public Guid Id { get; set; }
	public DateTime Start { get; set; }
	public DateTime End { get; set; }
	public Guid HostId { get; set; }
	public Guid ShowId { get; set; }
}
