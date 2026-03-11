namespace Infrastructure.Persistence;

using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;


public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IUnitOfWork
{
    public DbSet<Pokemon> Pokemons => Set<Pokemon>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    Task IUnitOfWork.SaveChangesAsync(CancellationToken ct)
    {
        return SaveChangesAsync(ct);
    }
}
