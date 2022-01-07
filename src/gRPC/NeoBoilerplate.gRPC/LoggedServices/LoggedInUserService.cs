using NeoBoilerplate.Application.Contracts;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;


namespace NeoBoilerplate.gRPC.LoggedServices
{
    public class LoggedInUserService : ILoggedInUserService
    {
        public LoggedInUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public string UserId { get; }
    }
}
