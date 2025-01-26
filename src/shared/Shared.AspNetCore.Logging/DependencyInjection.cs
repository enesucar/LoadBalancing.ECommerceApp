﻿using Shared.AspNetCore.Logging.Middlewares;
using Shared.AspNetCore.Logging.Models;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddCustomHttpLogging(
        this IServiceCollection services,
        Action<LoggingOptions> configureLoggingOptions)
    {   
        return services
            .Configure(configureLoggingOptions)
            .AddScoped<CustomHttpLoggingMiddleware>();
    }
}
