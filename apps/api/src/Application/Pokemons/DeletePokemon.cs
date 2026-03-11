namespace Application.Pokemons;

using Application.Interfaces;
using Domain.Repositories;

public record DeletePokemonCommand(Guid Id);

public static class DeletePokemonHandler
{
    public static async Task<bool> Handle(
        DeletePokemonCommand command,
        IPokemonRepository repository,
        IUnitOfWork unitOfWork,
        CancellationToken ct)
    {
        var pokemon = await repository.GetByIdForUpdateAsync(command.Id, ct);

        if (pokemon is null) return false;

        repository.Remove(pokemon);
        await unitOfWork.SaveChangesAsync(ct);

        return true;
    }
}
