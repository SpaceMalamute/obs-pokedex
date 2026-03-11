namespace Infrastructure.Persistence.Repositories;

using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

public class PokemonRepository(AppDbContext context) : IPokemonRepository
{
    public async Task<Pokemon?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await context.Pokemons.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id, ct);

    public async Task<IReadOnlyList<Pokemon>> GetAllAsync(CancellationToken ct = default)
        => await context.Pokemons.AsNoTracking().ToListAsync(ct);

    public async Task<(IReadOnlyList<Pokemon> Items, int Total)> GetPagedAsync(
        int page,
        int pageSize,
        string? search = null,
        PokemonType? type = null,
        CancellationToken ct = default)
    {
        var query = context.Pokemons.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(p => p.Name.Contains(search, StringComparison.CurrentCultureIgnoreCase));

        if (type is not null)
            query = query.Where(p => p.PrimaryType == type || p.SecondaryType == type);

        var total = await query.CountAsync(ct);

        var items = await query
            .OrderBy(p => p.PokedexNumber)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return (items, total);
    }

    public async Task<Pokemon?> GetByIdForUpdateAsync(Guid id, CancellationToken ct = default)
        => await context.Pokemons.FirstOrDefaultAsync(p => p.Id == id, ct);

    public async Task AddAsync(Pokemon pokemon, CancellationToken ct = default)
        => await context.Pokemons.AddAsync(pokemon, ct);

    public void Remove(Pokemon pokemon)
        => context.Pokemons.Remove(pokemon);
}
