namespace library___api.Models
{
    public class ErrorResponseMessage
    {
        //public object? Data { get; set; }
        public string? ErrorMessage { get; set; }
        
        //public ErrorResponseMessage FailureResponse(string message) => new ErrorResponseMessage() { ErrorMessage = message, Data= default };
        //public ErrorResponseMessage SuccessResponse(object data) => new ErrorResponseMessage() { ErrorMessage = default, Data = data };
    }
}
