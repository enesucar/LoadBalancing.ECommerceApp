using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Trendyum.Application.Interfaces;
using Trendyum.Domain.Entities;

namespace Trendyum.Infrastructure.Data.Contexts;

public class TrendyumDbContext :
    IdentityDbContext<
        User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>, ITrendyumDbContext
{
    public DbSet<Product> Products => Set<Product>();

    public DbSet<Category> Categories => Set<Category>();

    public DbSet<Basket> Baskets => Set<Basket>();

    public DbSet<BasketItem> BasketItems => Set<BasketItem>();

    public DbSet<Resource> Resources => Set<Resource>();

    public TrendyumDbContext(DbContextOptions<TrendyumDbContext> options) : base(options) { }

    public async Task<int> SaveChangesAsync()
    {
        return await base.SaveChangesAsync();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
