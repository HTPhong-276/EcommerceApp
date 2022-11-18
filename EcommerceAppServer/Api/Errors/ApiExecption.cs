namespace Api.Errors
{
    public class ApiExecption : ApiResponse
    {
        public ApiExecption(int statusCode, string message = null, string detail = null) : base(statusCode, message)
        {
            Detail = detail;
        }

        public string Detail { get; set; }
    }
}
