using Shared.Domain.Entities;

namespace Trendyum.Domain.Entities;

public class Category : Entity<Guid>
{
    public virtual string Name { get; set; }

    public Category()
    {
        Name = default!;
    }
}
