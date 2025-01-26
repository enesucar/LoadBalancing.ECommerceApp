using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared.Guids;
using Shared.Utils.Exceptions;
using Trendyum.Application.Interfaces;
using Trendyum.Application.Interfaces.Resources;
using Trendyum.Domain.Entities;

namespace Trendyum.Application.Resources;

public class ResourceService : IResourceService
{
    private readonly ITrendyumDbContext _trendyumDbContext;
    private readonly IStorage _storage;
    private readonly IGuidGenerator _guidGenerator;

    public ResourceService(
        ITrendyumDbContext trendyumDbContext,
        IStorage storage,
        IGuidGenerator guidGenerator)
    {
        _trendyumDbContext = trendyumDbContext;
        _storage = storage;
        _guidGenerator = guidGenerator;
    }

    public async Task<Resource> GetByIdAsync(Guid id)
    {
        var resource = await _trendyumDbContext.Resources.FirstOrDefaultAsync(o => o.Id == id);
        if (resource == null)
        {
            throw new NotFoundException();
        }
        return resource;
    }

    public async Task<Resource> CreateAsync(IFormFile file)
    {
        var fileName = await _storage.UploadAsync("files", file);
        var resource = new Resource()
        {
            Id = _guidGenerator.Generate(),
            Name = fileName,
            Path = "files",
            CreationDate = DateTime.Now,
            Size = file.Length,
            Type = file.ContentType
        };
        
        await _trendyumDbContext.Resources.AddAsync(resource);
        await _trendyumDbContext.SaveChangesAsync();

        return resource;
    }

    public async Task DeleteAsync(Guid id)
    {
        var resource = await _trendyumDbContext.Resources.FirstOrDefaultAsync(o => o.Id == id);
        if (resource == null)
        {
            throw new NotFoundException();
        }

        await _storage.DeleteAsync(resource.Path, resource.Name);
        await _trendyumDbContext.SaveChangesAsync();
    }
}
