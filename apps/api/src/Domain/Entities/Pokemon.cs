using Domain.Enums;

namespace Domain.Entities;

public class Pokemon
{
    public Guid Id { get; private set; }

    public int PokedexNumber
    {
        get; private set;
    }

    public string Name { get; private set; } = string.Empty;

    public PokemonType PrimaryType
    {
        get; private set;
    }

    public PokemonType? SecondaryType
    {
        get;
        private set;
    }

    public int Generation { get; private set; }

    public int HP { get; private set; }

    public int Attack { get; private set; }

    public int Defense { get; private set; }

    public int SpecialAttack
    {
        get; private set;
    }

    public int SpecialDefense
    {
        get; private set;
    }

    public int Speed { get; private set; }

    public string SpriteUrl { get; private set; } = string.Empty;

    public string Description
    {
        get; private set;
    } = string.Empty;

    public DateTime CreatedAt
    {
        get; private set;
    }

    public DateTime UpdatedAt
    {
        get; private set;
    }

    private Pokemon() { }

    public static Pokemon Create(
          int pokedexNumber,
          string name,
          PokemonType primaryType,
          PokemonType? secondaryType,
          int generation,
          int hp,
          int attack,
          int defense,
          int specialAttack,
          int specialDefense,
          int speed,
          string spriteUrl,
          string description)
    {
        var now = DateTime.UtcNow;

        return new Pokemon
        {
            Id = Guid.NewGuid(),
            PokedexNumber = pokedexNumber,
            Name = name,
            PrimaryType = primaryType,
            SecondaryType = secondaryType,
            Generation = generation,
            HP = hp,
            Attack = attack,
            Defense = defense,
            SpecialAttack = specialAttack,
            SpecialDefense = specialDefense,
            Speed = speed,
            SpriteUrl = spriteUrl,
            Description = description,
            CreatedAt = now,
            UpdatedAt = now
        };
    }

    public void Update(
        int pokedexNumber,
        string name,
        PokemonType primaryType,
        PokemonType? secondaryType,
        int generation,
        int hp,
        int attack,
        int defense,
        int specialAttack,
        int specialDefense,
        int speed,
        string spriteUrl,
        string description)
    {
        PokedexNumber = pokedexNumber;
        Name = name;
        PrimaryType = primaryType;
        SecondaryType = secondaryType;
        Generation = generation;
        HP = hp;
        Attack = attack;
        Defense = defense;
        SpecialAttack = specialAttack;
        SpecialDefense = specialDefense;
        Speed = speed;
        SpriteUrl = spriteUrl;
        Description = description;
        UpdatedAt = DateTime.UtcNow;
    }
}
