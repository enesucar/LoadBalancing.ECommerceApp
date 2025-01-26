using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using Steeltoe.Discovery.Client;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(
        this IServiceCollection services, 
        IHostBuilder hostBuilder,
        IConfiguration configuration)
    {
        services.AddControllers();
        services.AddOpenApi();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Trendyum API", Version = "v1" });
            options.AddSecurity();
            options.AddLowercaseDocumentFilter();
        });

        services.AddHttpContextCurrentPrincipalAccessor();
        services.AddHttpContextAccessor();

        services.AddEnableRequestBuffering();
        services.AddRequestTime();

        hostBuilder.UseSerilog((context, services, configuration) =>
        {
            configuration
                .WriteTo.Seq(context.Configuration.GetValue<string>("Seq:ServerUrl")!)
                .WriteTo.Console()
                .MinimumLevel.Verbose()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Fatal)
                .MinimumLevel.Override("Steeltoe", LogEventLevel.Fatal)
                .MinimumLevel.Override("System.Net.Http.HttpClient.Eureka", LogEventLevel.Fatal)
                .MinimumLevel.Override("Startup.Steeltoe.Http.HttpClient.Eureka", LogEventLevel.Fatal)
                .AddCustomEnriches();
        });

        services.AddPushSerilogProperties();

        services.AddCustomHttpLogging(options => { });

        services.AddCustomExceptionHandler();

        services.AddDiscoveryClient(configuration);

        services.AddAuthorization();

        services.AddCustomAuthentication(
            configuration.GetValue<string>("AccessTokenOptions:Audience")!,
            configuration.GetValue<string>("AccessTokenOptions:Issuer")!,
            configuration.GetValue<string>("AccessTokenOptions:SecurityKey")!);

        return services;
    }
}
