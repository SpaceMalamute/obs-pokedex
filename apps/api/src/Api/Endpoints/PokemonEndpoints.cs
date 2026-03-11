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

        group.MapGet("/", async (
            int? page,
            int? pageSize,
            string? search,
            Domain.Enums.PokemonType? type,
            IMessageBus bus,
            CancellationToken ct) =>
        {
            var result = await bus.InvokeAsync<PaginatedResponse<PokemonResponse>>(
                new GetAllPokemonsQuery(
                    Page: page ?? 1,
                    PageSize: pageSize ?? 20,
                    Search: search,
                    Type: type), ct);

            return TypedResults.Ok(result);
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
