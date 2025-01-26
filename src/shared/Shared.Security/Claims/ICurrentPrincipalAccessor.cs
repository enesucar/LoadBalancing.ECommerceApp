using System.Security.Claims;

namespace Shared.Security.Claims;

public interface ICurrentPrincipalAccessor
{
    ClaimsPrincipal ClaimsPrincipal { get; }
}
