using Microsoft.AspNetCore.Http;
using Trendyum.Domain.Entities;

namespace Trendyum.Application.Interfaces.Resources;

public interface IResourceService
{
    Task<Resource> GetByIdAsync(Guid id);

    Task<Resource> CreateAsync(IFormFile file);

    Task DeleteAsync(Guid id);
}
