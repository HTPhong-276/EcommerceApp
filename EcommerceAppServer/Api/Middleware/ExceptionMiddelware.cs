using Api.Errors;
using System.Net;
using System.Text.Json;

namespace Api.Middleware
{
    public class ExceptionMiddelware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddelware> logger;
        private readonly IHostEnvironment host;

        public ExceptionMiddelware(
            RequestDelegate next,
            ILogger<ExceptionMiddelware> logger,
            IHostEnvironment host)
        {
            this.next = next;
            this.logger = logger;
            this.host = host;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch(Exception e)
            {
                logger.LogError(e, e.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = host.IsDevelopment() ?
                    new ApiExecption((int)HttpStatusCode.InternalServerError, e.Message, e.StackTrace.ToString()) :
                    new ApiExecption((int)HttpStatusCode.InternalServerError);

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
