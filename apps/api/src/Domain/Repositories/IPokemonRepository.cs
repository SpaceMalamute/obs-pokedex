namespace Domain.Repositories;

using Domain.Entities;

public interface IPokemonRepository
{
    Task<Pokemon?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<Pokemon?> GetByIdForUpdateAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<Pokemon>> GetAllAsync(CancellationToken ct = default);
    Task AddAsync(Pokemon pokemon, CancellationToken ct = default);
    void Remove(Pokemon pokemon);
}
