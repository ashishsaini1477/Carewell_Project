using System.Net;
using System.Text.Json;

namespace CarwellAutoshop.CustomException
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NotFoundException ex)
            {
                await Handle(context, HttpStatusCode.NotFound, ex.Message);
            }
            catch (Exception ex)
            {
                await Handle(context, HttpStatusCode.InternalServerError, "Something went wrong");
            }
        }

        private static async Task Handle(HttpContext context, HttpStatusCode code, string message)
        {
            context.Response.StatusCode = (int)code;
            context.Response.ContentType = "application/json";

            var response = new
            {
                success = false,
                message
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
