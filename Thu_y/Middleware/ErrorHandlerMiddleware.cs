using System.Text.Json;
using Thu_y.Infrastructure.Utils.Exceptions;

namespace Thu_y.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;

                response.ContentType = "application/json";
                response.StatusCode = error switch
                {
                    AppException => (int)StatusCodes.Status400BadRequest
                };

                var result = JsonSerializer.Serialize(new { message = error?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}
