namespace Api.Endpoints;

using Application.Pokemons;
using Application.Pokemons.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using Wolverine;

public static class PokemonEndpoints
{
    public static void MapPokemonEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/v1/pokemons")
            .WithTags("Pokemons");

        group.MapGet("/", async (IMessageBus bus, CancellationToken ct) =>
        {
            var pokemons = await bus.InvokeAsync<IReadOnlyList<PokemonResponse>>(
                new GetAllPokemonsQuery(), ct);

            return TypedResults.Ok(pokemons);
        })
        .WithName("GetAllPokemons");

        group.MapGet("/{id:guid}", async Task<Results<Ok<PokemonResponse>, NotFound>> (Guid id, IMessageBus bus, CancellationToken ct) =>
        {
            var pokemon = await bus.InvokeAsync<PokemonResponse?>(
                new GetPokemonByIdQuery(id), ct);

            if (pokemon is null) return TypedResults.NotFound();

            return TypedResults.Ok(pokemon);
        }).WithName("GetPokemonById");

        group.MapPost("/", async (CreatePokemonCommand command, IMessageBus bus, CancellationToken ct) =>
        {
            var pokemon = await bus.InvokeAsync<PokemonResponse>(command, ct);

            return TypedResults.Created($"/api/v1/pokemons/{pokemon.Id}", pokemon);
        }).WithName("CreatePokemon");

        group.MapPut("/{id:guid}", async Task<Results<Ok<PokemonResponse>, NotFound, BadRequest>> (Guid id, UpdatePokemonCommand command, IMessageBus bus, CancellationToken ct) =>
        {
            if (id != command.Id) return TypedResults.BadRequest();

            var pokemon = await bus.InvokeAsync<PokemonResponse?>(command, ct);

            if (pokemon is null) return TypedResults.NotFound();

            return TypedResults.Ok(pokemon);
        }).WithName("UpdatePokemon");

        group.MapDelete("/{id:guid}", async Task<Results<NoContent, NotFound>> (Guid id, IMessageBus bus, CancellationToken ct) =>
        {
            var deleted = await bus.InvokeAsync<bool>(new DeletePokemonCommand(id), ct);

            if (!deleted) return TypedResults.NotFound();

            return TypedResults.NoContent();
        }).WithName("DeletePokemon");
    }
}
