using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Shared.AspNetCore.Handlers;
using Shared.AspNetCore.Middlewares;
using Shared.AspNetCore.Security.Claims;
using Shared.Security.Claims;
using System;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddEnableRequestBuffering(this IServiceCollection services)
    {
        return services.AddScoped<EnableRequestBufferingMiddleware>();
    }

    public static IServiceCollection AddRequestTime(this IServiceCollection services)
    {
        return services.AddScoped<RequestTimeMiddleware>();
    }

    public static IServiceCollection AddHttpContextCurrentPrincipalAccessor(this IServiceCollection services)
    {
        return services.AddScoped<ICurrentPrincipalAccessor, HttpContextCurrentPrincipalAccessor>();
    }

    public static IServiceCollection AddPushSerilogProperties(this IServiceCollection services)
    {
        return services.AddScoped<PushSerilogPropertiesMiddleware>();
    }

    public static IServiceCollection AddCustomExceptionHandler(this IServiceCollection services)
    {
        return services.AddExceptionHandler<CustomExceptionHandler>();
    }

    public static IServiceCollection AddCustomAuthentication(
      this IServiceCollection services,
      string audience,
      string issuer,
      string issuerSigningKey)
    {
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = audience,
                    ValidIssuer = issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(issuerSigningKey)),
                    ClockSkew = TimeSpan.Zero
                };
            });

        return services;
    }
}
