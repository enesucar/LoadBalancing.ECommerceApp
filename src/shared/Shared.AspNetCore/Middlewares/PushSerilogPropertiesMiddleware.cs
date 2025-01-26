using Serilog.Context;
using Shared.AspNetCore.HttpFeatures;
using System.Net;
using System.Security.Claims;

namespace Shared.AspNetCore.Middlewares;

public class PushSerilogPropertiesMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var httpRequestTimeFeature = context.Features.Get<IHttpRequestTimeFeature>();

        LogContext.PushProperty("Host", context.Request.Host.ToString());
        LogContext.PushProperty("UserId", context.User.Claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value.ToGuid() ?? null);
        LogContext.PushProperty("ClientIPAddress", context.Connection.RemoteIpAddress?.ToString());
        LogContext.PushProperty("ClientName", Dns.GetHostEntry(context.Connection.RemoteIpAddress?.ToString()!).HostName);
        LogContext.PushProperty("CorrelationId", context.TraceIdentifier);
        LogContext.PushProperty("RequestQueryString", context.Request.GetQueryString());
        LogContext.PushProperty("RequestMethod", context.Request.Method);
        LogContext.PushProperty("RequestExecutionTime", httpRequestTimeFeature!.RequestDate);
        //LogContext.PushProperty("AcceptLanguage", context.Request.GetHeader(HeaderConstants.AcceptLanguage));
        //LogContext.PushProperty("TimeZoneIdentifier", context.Request.GetHeader(HeaderConstants.TimeZoneIdentifier));
        //LogContext.PushProperty("UserAgent", context.Request.GetHeader(HeaderConstants.UserAgent));
        LogContext.PushProperty("RequestMethod", context.Request.Method);

        await next(context);
    }
}
