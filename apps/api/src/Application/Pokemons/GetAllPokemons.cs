namespace Application.Pokemons;

using Application.Pokemons.Dtos;
using Domain.Enums;
using Domain.Repositories;

public record GetAllPokemonsQuery(
    int Page = 1,
    int PageSize = 20,
    string? Search = null,
    PokemonType? Type = null);

public static class GetAllPokemonsHandler
{
    public static async Task<PaginatedResponse<PokemonResponse>> Handle(
        GetAllPokemonsQuery query,
        IPokemonRepository repository,
        CancellationToken ct)
    {
        var (pokemons, total) = await repository.GetPagedAsync(
            query.Page,
            query.PageSize,
            query.Search,
            query.Type,
            ct);

        var items = pokemons.Select(p => new PokemonResponse(
            p.Id,
            p.PokedexNumber,
            p.Name,
            p.PrimaryType,
            p.SecondaryType,
            p.Generation,
            p.HP,
            p.Attack,
            p.Defense,
            p.SpecialAttack,
            p.SpecialDefense,
            p.Speed,
            p.SpriteUrl,
            p.Description)).ToList();

        return new PaginatedResponse<PokemonResponse>(items, total, query.Page, query.PageSize);
    }
}
