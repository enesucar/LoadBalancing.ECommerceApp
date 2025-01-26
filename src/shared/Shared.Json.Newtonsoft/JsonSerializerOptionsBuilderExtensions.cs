using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Shared.Json.Newtonsoft;

namespace Shared.Json;

public static class JsonSerializerOptionsBuilderExtensions
{
    public static void UseNewtonsoft(
        this JsonSerializerOptionsBuilder jsonSerializerOptionsBuilder,
        IServiceCollection services,
        Action<JsonSerializerSettings> configureJsonSerializerSettings)
    {
        services
            .Configure(configureJsonSerializerSettings)
            .AddSingleton<IJsonSerializer, NewtonsoftSerializer>();
    }
}
