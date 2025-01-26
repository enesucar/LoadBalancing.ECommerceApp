using Shared.AspNetCore.Middlewares;

namespace Microsoft.AspNetCore.Builder;

public static class IApplicationBuilderExtensions
{
    public static IApplicationBuilder UseRequestTime(this IApplicationBuilder applicationBuilder)
    {
        return applicationBuilder.UseMiddleware<RequestTimeMiddleware>();
    }

    public static IApplicationBuilder UseEnableRequestBuffering(this IApplicationBuilder applicationBuilder)
    {
        return applicationBuilder.UseMiddleware<EnableRequestBufferingMiddleware>();
    }

    public static IApplicationBuilder UsePushSerilogPropertiesMiddleware(this IApplicationBuilder applicationBuilder)
    {
        return applicationBuilder.UseMiddleware<PushSerilogPropertiesMiddleware>();
    }
}
