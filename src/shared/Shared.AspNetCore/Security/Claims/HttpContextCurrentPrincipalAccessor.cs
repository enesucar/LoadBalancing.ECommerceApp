using System.Security.Claims;
using Shared.Security.Claims;

namespace Shared.AspNetCore.Security.Claims;

public class HttpContextCurrentPrincipalAccessor : ICurrentPrincipalAccessor
{
    public ClaimsPrincipal ClaimsPrincipal { get; }

    public HttpContextCurrentPrincipalAccessor(IHttpContextAccessor httpContextAccessor)
    {
        ClaimsPrincipal = httpContextAccessor.HttpContext?.User ?? (Thread.CurrentPrincipal as ClaimsPrincipal)!;
    }
}
