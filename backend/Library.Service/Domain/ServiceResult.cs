using System.Net;

namespace library.Services.Domain
{
    public class ServiceResult<T> 
    {
        public bool IsSuccess { get; } = false;
        public HttpStatusCode Status { get; set; }
        public T? Data { get; set; }    = default;
        public string? ErrorMessage { get; set; } = default;

        // Don't allow object initializer
        //public ServiceResult() { }
        private ServiceResult(HttpStatusCode statusCode, T? result , string? errorMessage, bool isSuccess)
        {
            Status = statusCode;
            Data = result;
            ErrorMessage = errorMessage;
            IsSuccess = isSuccess;
        }

        public static ServiceResult<T> SetFailureServiceResult(HttpStatusCode statusCode, string? errorMessage)
            =>  new ServiceResult<T>(statusCode, default(T), errorMessage, false);
        public static ServiceResult<T> SetSuccessServiceResult(HttpStatusCode statusCode, T? result) 
            =>new ServiceResult<T>(statusCode, result, default, true);
 
    }

}
