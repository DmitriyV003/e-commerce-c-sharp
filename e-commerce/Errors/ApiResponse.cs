namespace e_commerce.Errors;

public class ApiResponse
{
    public ApiResponse(int statusCode, string? message)
    {
        StatusCode = statusCode;
        Message = message ?? GetDefaultMessageForStatusCode(statusCode);
    }

    private string GetDefaultMessageForStatusCode(int statusCode)
    {
        return statusCode switch
        {
            400 => "Bad request",
            401 => "Not authorized",
            _ => "Unknown Error"
        };
    }

    public int StatusCode { get; set; }
    public string Message { get; set; }
}
