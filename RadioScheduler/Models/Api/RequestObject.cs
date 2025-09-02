namespace RadioScheduler.Models.Api;

public class RequestObject<T> {
	public string? RequestId { get; set; }
	public T? Data { get; set; }
}
