using Shared.AspNetCore.Logging.Middlewares;

namespace Microsoft.AspNetCore.Builder;

public static class IApplicationBuilderExtensions
{
    public static IApplicationBuilder UseCustomHttpLoggingMiddleware(this IApplicationBuilder applicationBuilder)
    {
        return applicationBuilder.UseMiddleware<CustomHttpLoggingMiddleware>();
    }
}
