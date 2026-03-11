namespace Application.Pokemons.Dtos;

using Domain.Enums;

public record PokemonResponse(
    Guid Id,
    int PokedexNumber,
    string Name,
    PokemonType PrimaryType,
    PokemonType? SecondaryType,
    int Generation,
    int HP,
    int Attack,
    int Defense,
    int SpecialAttack,
    int SpecialDefense,
    int Speed,
    string SpriteUrl,
    string Description);
