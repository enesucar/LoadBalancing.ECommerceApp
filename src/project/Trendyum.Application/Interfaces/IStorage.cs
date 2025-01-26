using Microsoft.AspNetCore.Http;

namespace Trendyum.Application.Interfaces;

public interface IStorage
{
    Task<string> UploadAsync(string path, IFormFile file);
    
    Task DeleteAsync(string path, string fileName);
    
    List<string> GetFiles(string path);
    
    bool HasFile(string path, string fileName);
}
