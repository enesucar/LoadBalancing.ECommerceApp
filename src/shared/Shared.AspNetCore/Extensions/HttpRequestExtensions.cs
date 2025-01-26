using Microsoft.Extensions.Primitives;
using System.Text;

namespace Microsoft.AspNetCore.Http;

public static class HttpRequestExtensions
{
    public static string? GetQueryString(this HttpRequest httpRequest)
    {
        string? queryString = httpRequest.QueryString.ToString();
        return queryString.IsNullOrWhiteSpace() ? null : queryString;
    }

    public static string? GetHeader(this HttpRequest httpRequest, string key)
    {
        if (httpRequest.Headers.Keys.Count == 0)
        {
            return null;
        }

        if (!httpRequest.Headers.TryGetValue(key, out StringValues value))
        {
            return null;
        }

        return value.FirstOrDefault();
    }

    public static string? GetHeaders(this HttpRequest httpRequest)
    {
        if (httpRequest.Headers.Keys.Count == 0)
        {
            return null;
        }

        string? header = httpRequest.Headers.Select(x => x.ToString()).Aggregate((key, value) => key + ", " + value);
        return string.IsNullOrEmpty(header) ? null : header;
    }

    public async static Task<string?> GetBodyAsync(this HttpRequest httpRequest)
    {
        string? requestBody = null;
        httpRequest.Body.Position = 0;
        using (StreamReader reader = new StreamReader(httpRequest.Body, Encoding.UTF8, true, 1024, true))
        {
            requestBody = await reader.ReadToEndAsync();
        }
        httpRequest.Body.Position = 0;
        return requestBody == string.Empty ? null : requestBody;
    }
}
