namespace RadioScheduler.Models.Api;

public class MetaData {
	public Guid RequestId { get; set; } = Guid.NewGuid();
	public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
