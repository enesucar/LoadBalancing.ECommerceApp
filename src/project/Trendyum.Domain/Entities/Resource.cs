using Shared.Domain.Entities;

namespace Trendyum.Domain.Entities;

public class Resource : Entity<Guid>
{
    public string Name { get; set; }

    public string Path { get; set; }

    public string Type { get; set; }

    public long Size { get; set; }

    public DateTime CreationDate { get; set; }

    public Resource()
    {
        Name = default!;
        Path = default!;
        Type = default!;
    }
}
