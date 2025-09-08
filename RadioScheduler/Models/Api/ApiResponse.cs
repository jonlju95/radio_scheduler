namespace RadioScheduler.Models.Api;

public class ApiResponse {
	public bool Success { get; set; } = true;
	public object? Data { get; set; }
	public MetaData Meta { get; private set; } = new MetaData();
	public List<ErrorInfo?> Error { get; private set; } = [];
}
