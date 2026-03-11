using Api.Endpoints;
using Infrastructure;
using Wolverine;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddInfrastructure(
      builder.Configuration.GetConnectionString("DefaultConnection")!);

builder.Host.UseWolverine(opts =>
 {
     opts.Discovery.IncludeAssembly(typeof(Application.DependencyInjection).Assembly);
 });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

// Map endpoints
app.MapPokemonEndpoints();

app.UseHttpsRedirection();

app.Lifetime.ApplicationStarted.Register(() =>
{
    var baseUrl = app.Urls.FirstOrDefault() ?? "http://localhost:5000";
    var api = $"{baseUrl}/api/v1/pokemons";
    var docs = $"{baseUrl}/scalar/v1";
    var esc = "\x1b";
    Console.WriteLine();
    Console.WriteLine($"    {esc}[32m🚀 Pokedex API running{esc}[0m");
    Console.WriteLine();
    Console.WriteLine($"       {esc}[90mAPI   {esc}[0m {esc}[36m{api}{esc}[0m");
    Console.WriteLine($"       {esc}[90mDocs  {esc}[0m {esc}[36m{docs}{esc}[0m");
    Console.WriteLine();
});

app.Run();
