namespace RadioScheduler.Models.Api;

public class ResponseObject<T> {
	public bool Success { get; set; }
	public string? RequestId { get; set; }
	public ErrorInfo? Error { get; set; }
	public T? Data { get; set; }

	public static ResponseObject<T> Ok(T data) => new ResponseObject<T> { Success = true, Data = data };

	public static ResponseObject<T> Fail(ErrorInfo error) => new ResponseObject<T> { Success = false, Error = error };
}
