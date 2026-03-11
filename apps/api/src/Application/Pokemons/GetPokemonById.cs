namespace Application.Pokemons;

using Application.Pokemons.Dtos;
using Domain.Repositories;

public record GetPokemonByIdQuery(Guid Id);

public static class GetPokemonByIdHandler
{
    public static async Task<PokemonResponse?> Handle(
        GetPokemonByIdQuery query,
        IPokemonRepository repository,
        CancellationToken ct)
    {
        var pokemon = await repository.GetByIdAsync(query.Id, ct);

        if (pokemon is null) return null;

        return new PokemonResponse(
            pokemon.Id,
            pokemon.PokedexNumber,
            pokemon.Name,
            pokemon.PrimaryType,
            pokemon.SecondaryType,
            pokemon.Generation,
            pokemon.HP,
            pokemon.Attack,
            pokemon.Defense,
            pokemon.SpecialAttack,
            pokemon.SpecialDefense,
            pokemon.Speed,
            pokemon.SpriteUrl,
            pokemon.Description);
    }
}
