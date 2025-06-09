using System.Net;

namespace AstroBot.Application.DTOs.Responses
{
    public class ResponseBase<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public T? Data { get; set; }
        public string? ErrorMessage { get; set; }

        public static ResponseBase<T> Success(T data, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new ResponseBase<T>
            {
                StatusCode = statusCode,
                Data = data,
                ErrorMessage = null
            };
        }

        public static ResponseBase<T> Error(string errorMessage, HttpStatusCode statusCode)
        {
            return new ResponseBase<T>
            {
                StatusCode = statusCode,
                Data = default,
                ErrorMessage = errorMessage
            };
        }
    }
} 