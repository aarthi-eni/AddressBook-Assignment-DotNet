using Microsoft.AspNetCore.Http;
using Entities.Dtos;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CustomExceptionHandling
{
    public class ExceptionDetails
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public ExceptionDetails(RequestDelegate next, ILogger logger)
        {
            _logger = logger;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ExceptionModel ex)
            {
                _logger.LogError(ex.error.Message);
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, ExceptionModel exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exception.error.StatusCode;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorDto()
            {
                StatusCode = exception.error.StatusCode,
                Description = exception.error.Description,
                Message = exception.error.Message
            }));
        }
    }
}