namespace Infrastructure.PokeApi;

using Domain.Entities;
using Domain.Enums;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using System.Net.Http.Json;
using Infrastructure.Dtos;
using System.Text.Json;

public class PokemonSeeder(
    IServiceScopeFactory scopeFactory,
    HttpClient httpClient,
    ILogger<PokemonSeeder> logger) : IHostedService
{
    private const string BaseUrl = "https://pokeapi.co/api/v2";

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    public async Task StartAsync(CancellationToken ct)
    {
        await using var scope = scopeFactory.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (await context.Pokemons.AnyAsync(ct))
        {
            logger.LogInformation("Database already seeded, skipping.");
            return;
        }

        logger.LogInformation("Seeding database from PokeAPI...");

        var listResponse = await httpClient.GetFromJsonAsync<PokeApiListResponse>(
            $"{BaseUrl}/pokemon?limit=100000", JsonOptions, ct);

        if (listResponse is null) return;

        var pokemonNames = listResponse.Results.ToDictionary(
            item => int.Parse(item.Url.TrimEnd('/').Split('/').Last()),
            item => item.Name);

        var pokemonIds = pokemonNames.Keys.Where(id => id <= 1025).ToList();
        var lockObj = new object();

        const int batchSize = 20;

        foreach (var batch in pokemonIds.Chunk(batchSize))
        {
            var tasks = batch.Select(async id =>
            {
                try
                {
                    var pokemon = await httpClient.GetFromJsonAsync<PokeApiPokemonResponse>(
                        $"{BaseUrl}/pokemon/{id}", JsonOptions, ct);
                    var species = await httpClient.GetFromJsonAsync<PokeApiSpeciesResponse>(
                        $"{BaseUrl}/pokemon-species/{id}", JsonOptions, ct);

                    if (pokemon is null || species is null) return;

                    var primaryType = ParseType(pokemon.Types.First(t => t.Slot == 1).Type.Name);
                    var secondaryType = pokemon.Types.FirstOrDefault(t => t.Slot == 2) is { } second
                        ? ParseType(second.Type.Name)
                        : (PokemonType?)null;

                    var stats = pokemon.Stats.ToDictionary(s => s.Stat.Name, s => s.BaseStat);

                    var description = species.FlavorTextEntries
                        .FirstOrDefault(f => f.Language.Name == "en")?.FlavorText ?? "";
                    description = description.Replace("\n", " ").Replace("\f", " ");

                    var generation = ParseGeneration(species.Generation.Name);

                    var entity = Pokemon.Create(
                        pokedexNumber: id,
                        name: pokemonNames[id],
                        primaryType: primaryType,
                        secondaryType: secondaryType,
                        generation: generation,
                        hp: stats.GetValueOrDefault("hp"),
                        attack: stats.GetValueOrDefault("attack"),
                        defense: stats.GetValueOrDefault("defense"),
                        specialAttack: stats.GetValueOrDefault("special-attack"),
                        specialDefense: stats.GetValueOrDefault("special-defense"),
                        speed: stats.GetValueOrDefault("speed"),
                        spriteUrl: pokemon.Sprites.FrontDefault ?? "",
                        description: description);

                    lock (lockObj)
                    {
                        context.Pokemons.Add(entity);
                    }

                    logger.LogInformation("Seeded #{Id} {Name}", id, pokemonNames[id]);
                }
                catch (Exception ex)
                {
                    logger.LogWarning(ex, "Failed to seed #{Id}, skipping.", id);
                }
            });

            await Task.WhenAll(tasks);
        }

        await context.SaveChangesAsync(ct);
        logger.LogInformation("Seeding complete.");
    }

    public Task StopAsync(CancellationToken ct) => Task.CompletedTask;

    private static PokemonType ParseType(string name)
        => Enum.Parse<PokemonType>(name, ignoreCase: true);

    private static int ParseGeneration(string name)
    {
        var roman = name.Split('-').Last();
        return roman switch
        {
            "i" => 1,
            "ii" => 2,
            "iii" => 3,
            "iv" => 4,
            "v" => 5,
            "vi" => 6,
            "vii" => 7,
            "viii" => 8,
            "ix" => 9,
            _ => 0
        };
    }
}
