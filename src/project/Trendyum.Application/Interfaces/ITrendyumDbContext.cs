using Microsoft.EntityFrameworkCore;
using Trendyum.Domain.Entities;

namespace Trendyum.Application.Interfaces;

public interface ITrendyumDbContext
{
    DbSet<Basket> Baskets { get; }

    DbSet<Category> Categories { get; }

    DbSet<Product> Products { get; }

    DbSet<Resource> Resources { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
