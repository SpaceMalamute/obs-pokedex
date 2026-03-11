namespace Application.Pokemons;

using Application.Interfaces;
using Application.Pokemons.Dtos;
using Domain.Enums;
using Domain.Repositories;

public record UpdatePokemonCommand(
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

public static class UpdatePokemonHandler
{
    public static async Task<PokemonResponse?> Handle(
        UpdatePokemonCommand command,
        IPokemonRepository repository,
        IUnitOfWork unitOfWork,
        CancellationToken ct)
    {
        var pokemon = await repository.GetByIdForUpdateAsync(command.Id, ct);

        if (pokemon is null) return null;

        pokemon.Update(
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
