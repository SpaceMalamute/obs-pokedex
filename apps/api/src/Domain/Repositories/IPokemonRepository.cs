namespace Domain.Repositories;

using Domain.Entities;
using Domain.Enums;

public interface IPokemonRepository
{
    Task<Pokemon?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<Pokemon?> GetByIdForUpdateAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<Pokemon>> GetAllAsync(CancellationToken ct = default);
    Task<(IReadOnlyList<Pokemon> Items, int Total)> GetPagedAsync(
        int page,
        int pageSize,
        string? search = null,
        PokemonType? type = null,
        CancellationToken ct = default);
    Task AddAsync(Pokemon pokemon, CancellationToken ct = default);
    void Remove(Pokemon pokemon);
}
