using Resturant_Api.HandleErrors;
using System.Net;
using System.Text.Json;

namespace Resturant_Api.MiddleWare
{
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleWare> logger;
        private readonly IHostEnvironment env;

        public ExceptionMiddleWare(RequestDelegate Next ,ILogger<ExceptionMiddleWare> logger , IHostEnvironment env)
        {
            next = Next;
            this.logger = logger;
            this.env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var Response = env.IsDevelopment() ? 
                    new ApiExceptionError((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace?.ToString()) : 
                    new ApiExceptionError((int)HttpStatusCode.InternalServerError);
                var options = new JsonSerializerOptions 
                { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(Response, options);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
