namespace Application.Pokemons;

using Application.Interfaces;
using Application.Pokemons.Dtos;
using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;

public record CreatePokemonCommand(
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

public static class CreatePokemonHandler
{
    public static async Task<PokemonResponse> Handle(
        CreatePokemonCommand command,
        IPokemonRepository repository,
        IUnitOfWork unitOfWork,
        CancellationToken ct)
    {
        var pokemon = Pokemon.Create(
            pokedexNumber: command.PokedexNumber,
            name: command.Name,
            primaryType: command.PrimaryType,
            secondaryType: command.SecondaryType,
            generation: command.Generation,
            hp: command.HP,
            attack: command.Attack,
            defense: command.Defense,
            specialAttack: command.SpecialAttack,
            specialDefense: command.SpecialDefense,
            speed: command.Speed,
            spriteUrl: command.SpriteUrl,
            description: command.Description);

        await repository.AddAsync(pokemon, ct);

        await unitOfWork.SaveChangesAsync(ct);

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
