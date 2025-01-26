using Shared.Json;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddJsonSerializer(
        this IServiceCollection services,
        Action<JsonSerializerOptionsBuilder> configureJsonSerializerOptions)
    {
        JsonSerializerOptionsBuilder jsonSerializerOptionsBuilder = new JsonSerializerOptionsBuilder();
        configureJsonSerializerOptions(jsonSerializerOptionsBuilder);
        return services;
    }
}
