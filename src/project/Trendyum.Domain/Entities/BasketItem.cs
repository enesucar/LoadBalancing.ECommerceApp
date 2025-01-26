using Shared.Domain.Entities;

namespace Trendyum.Domain.Entities;

public class BasketItem : Entity
{
    public Guid BasketId { get; set; }

    public Guid ProductId { get; set; }

    public Product? Product { get; set; }
}
