using Shared.Guids;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddCustomGuidGenerator(this IServiceCollection services)
    {
        return services.AddSingleton<IGuidGenerator, CustomGuidGenerator>();
    }

    public static IServiceCollection AddSequentialGuidGenerator(this IServiceCollection services)
    {
        return services.AddSingleton<IGuidGenerator, SequentialGuidGenerator>();
    }
}
