using System;

namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetHashCodeDefaultMessageForStatusCode(statusCode);
        }

        private string GetHashCodeDefaultMessageForStatusCode(int statusCode)
        {
          return statusCode switch 
          {
              400 => "A bad request, you have made",
              401 => "Authorized, you are not",
              404 => "Resource found, it was not",
              500 => "Errors are the path to dark side. Errors lead to anger. anger leads to hate. hate leads to carres change",
              _ => null,
          };
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        
    }
}