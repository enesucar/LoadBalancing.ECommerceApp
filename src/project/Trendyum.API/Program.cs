var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebServices(builder.Host, builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        { 
            builder.AllowAnyOrigin();
            builder.AllowAnyHeader();
            builder.AllowAnyMethod();
        });
});

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILogger<Program>>();
var appStartedLogMessage = "{ApplicationName} started. Hosting environment: {Environment}";
logger.LogInformation(appStartedLogMessage);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    await app.InitializeDatabaseAsync();
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Trendyum API");
    });
}

app.UseEnableRequestBuffering();

app.UseRequestTime();

app.UsePushSerilogPropertiesMiddleware();

app.UseCustomHttpLoggingMiddleware();

//app.UseHttpsRedirection();

app.UseCors(
    );


app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandler(o => { });

app.Run();
