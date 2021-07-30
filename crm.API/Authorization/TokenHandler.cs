using System.Threading.Tasks;
using crm.API.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace crm.API.Authorization
{
    public class TokenHandler : AuthorizationHandler<TokenRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TokenRequirement requirement)
        {
            if (context.Resource is DefaultHttpContext httpContext)
            {
                if (GetBearer(httpContext.Request) == requirement.Bearer)
                {
                    context.Succeed(requirement);
                }
                else
                {
                    throw new UnauthorizedException("Nieprawidłowy token.");
                }
            }
            return Task.CompletedTask;
        }

        private string GetBearer(HttpRequest request)
        {
            string authorization = request.Headers["Authorization"];

            if (string.IsNullOrEmpty(authorization) || !authorization.ToLower().StartsWith("bearer "))
            {
                return string.Empty;
            }

            return authorization.Substring(7);
        }
    }
}
