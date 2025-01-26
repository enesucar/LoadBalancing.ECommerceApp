using Shared.Domain.Entities;

namespace Trendyum.Domain.Entities;

public class Basket : Entity<Guid>
{
    public virtual Guid UserId { get; set; }

    public virtual ICollection<BasketItem> BasketItems { get; set; }

    public Basket()
    {
        BasketItems = [];
    }
}
