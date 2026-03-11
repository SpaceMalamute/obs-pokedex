namespace Infrastructure.Dtos;

public record PokeApiListResponse(List<PokeApiListItem> Results);

public record PokeApiListItem(string Name, string Url);

public record PokeApiPokemonResponse(
    int Id,
    List<PokeApiTypeSlot> Types,
    List<PokeApiStat> Stats,
    PokeApiSprites Sprites);

public record PokeApiTypeSlot(int Slot, PokeApiNamedResource Type);

public record PokeApiStat(int BaseStat, PokeApiNamedResource Stat);

public record PokeApiSprites(string? FrontDefault);

public record PokeApiSpeciesResponse(
    PokeApiNamedResource Generation,
    List<PokeApiFlavorText> FlavorTextEntries);

public record PokeApiFlavorText(string FlavorText, PokeApiNamedResource Language);

public record PokeApiNamedResource(string Name);
