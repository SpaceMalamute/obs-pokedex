namespace Application.Pokemons.Dtos;

public record PaginatedResponse<T>(
    IReadOnlyList<T> Items,
    int Total,
    int Page,
    int PageSize);
