namespace Api.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessage(statusCode);
        }

        private string GetDefaultMessage(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad request!",
                401 => "Authorized error!",
                404 => "Resource found, it was not",
                500 => "Server error!",
                _ => null
            };
        }
    }
}
