using System.Net;
using System.Threading.Tasks;
using crm.API.Exceptions;
using Microsoft.AspNetCore.Http;

namespace crm.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (UnauthorizedException unauthorizedException)
            {
                context.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync(unauthorizedException.Message);
            }
            catch
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong");
            }
        }
    }
}
