namespace Infrastructure;

using Application.Interfaces;
using Domain.Repositories;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Infrastructure.PokeApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<AppDbContext>(options =>
              options.UseNpgsql(connectionString));

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<AppDbContext>());
        services.AddScoped<IPokemonRepository, PokemonRepository>();

        services.AddHttpClient<PokemonSeeder>();
        services.AddHostedService<PokemonSeeder>();


        return services;
    }
}
