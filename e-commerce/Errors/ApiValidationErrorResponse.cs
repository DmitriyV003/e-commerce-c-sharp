namespace e_commerce.Errors;

public class ApiValidationErrorResponse : ApiResponse
{
    public IEnumerable<string> Errors { get; set; }

    public ApiValidationErrorResponse(int statusCode, IEnumerable<string> errors, string? message = null) : base(statusCode, message)
    {
        Errors = errors;
    }
}