using Microsoft.AspNetCore.Identity;

namespace Trendyum.Domain.Entities;

public class User : IdentityUser<Guid>
{
    public DateTime CreationDate { get; set; }
}
