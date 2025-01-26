using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Eureka;

var builder = WebApplication.CreateBuilder(args);

builder.Services
         .AddOcelot()
         .AddEureka();

builder.Configuration.AddJsonFile("ocelot.json");

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

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors();

await app.UseOcelot();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
