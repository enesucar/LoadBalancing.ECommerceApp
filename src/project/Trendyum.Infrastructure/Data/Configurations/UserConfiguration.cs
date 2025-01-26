using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trendyum.Domain.Entities;

namespace Trendyum.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users", "Identity");
        builder.Property(p => p.Id).HasColumnType("uniqueidentifier");
        builder.Property(p => p.Email).IsRequired();
        builder.Property(p => p.NormalizedEmail).IsRequired();
        builder.Property(p => p.CreationDate).HasColumnType("datetime").IsRequired();
    }
}
