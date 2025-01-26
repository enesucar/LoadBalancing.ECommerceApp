using Shared.Security.Tokens;
using Shared.Security.Users;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddSecurity(
        this IServiceCollection services,
        Action<AccessTokenOptions> configureAccessTokenOptions)
    {
        return services
            .Configure(configureAccessTokenOptions)
            .AddSingleton<ITokenService, JwtService>()
            .AddScoped<ICurrentUser, CurrentUser>();
    }
}
