namespace RadioScheduler.Models.Api;

public class ErrorInfo(string code, string message) {
	public string Code { get; set; } = code;
	public string Message { get; set; } = message;
}
