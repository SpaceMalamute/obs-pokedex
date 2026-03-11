namespace Application.Pokemons;

using Application.Pokemons.Dtos;
using Domain.Repositories;

public record GetAllPokemonsQuery;

public static class GetAllPokemonsHandler
{
    public static async Task<IReadOnlyList<PokemonResponse>> Handle(
        GetAllPokemonsQuery query,
        IPokemonRepository repository,
        CancellationToken ct)
    {
        var pokemons = await repository.GetAllAsync(ct);

        return [.. pokemons.Select(p => new PokemonResponse(
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
            p.Description))];
    }
}
