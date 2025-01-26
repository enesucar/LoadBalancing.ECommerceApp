using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Trendyum.Domain.Entities;

namespace Trendyum.Infrastructure.Data.Configurations;

public class BasketConfiguration : IEntityTypeConfiguration<Basket>
{
    public void Configure(EntityTypeBuilder<Basket> builder)
    {
        builder.ToTable("Baskets", "Trendyum").HasKey(o => o.Id);
        builder.Property(p => p.Id).HasColumnType("uniqueidentifier");
        builder.Property(p => p.UserId).HasColumnType("uniqueidentifier");
    }
}