using Shared.Domain.Entities;

namespace Trendyum.Domain.Entities;

public class Product : Entity<Guid>
{
    public virtual string Name { get; set; }

    public virtual string Description { get; set; }

    public virtual Guid CategoryId { get; set; }

    public virtual decimal Price { get; set; }

    public virtual int Quantity { get; set; }

    public virtual Guid? ImageId { get; set; }

    public virtual DateTime CreationDate { get; set; }

    public virtual Category Category { get; set; }

    public virtual Resource? Image { get; set; }

    public Product()
    {
        Name = default!;
        Description = default!;
        Category = default!;
    }
}
