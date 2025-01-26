namespace Shared.AspNetCore.HttpFeatures;

public class HttpRequestTimeFeature : IHttpRequestTimeFeature
{
    public DateTime RequestDate { get; }

    public HttpRequestTimeFeature()
    {
        RequestDate = DateTime.Now;
    }
}
