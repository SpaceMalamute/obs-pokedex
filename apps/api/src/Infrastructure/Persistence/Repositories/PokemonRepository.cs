namespace Infrastructure.Persistence.Repositories;

using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

public class PokemonRepository(AppDbContext context) : IPokemonRepository
{
    public async Task<Pokemon?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await context.Pokemons.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id, ct);

    public async Task<IReadOnlyList<Pokemon>> GetAllAsync(CancellationToken ct = default)
        => await context.Pokemons.AsNoTracking().ToListAsync(ct);

    public async Task<Pokemon?> GetByIdForUpdateAsync(Guid id, CancellationToken ct = default)
        => await context.Pokemons.FirstOrDefaultAsync(p => p.Id == id, ct);

    public async Task AddAsync(Pokemon pokemon, CancellationToken ct = default)
        => await context.Pokemons.AddAsync(pokemon, ct);

    public void Remove(Pokemon pokemon)
        => context.Pokemons.Remove(pokemon);
}
