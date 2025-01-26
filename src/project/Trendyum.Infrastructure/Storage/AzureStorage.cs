using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Shared.Guids;
using Trendyum.Application.Interfaces;

namespace Trendyum.Infrastructure.Storage;

public class AzureStorage : IStorage
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly IGuidGenerator _guidGenerator;

    public AzureStorage(IGuidGenerator guidGenerator)
    {
        _blobServiceClient = new BlobServiceClient("");
        _guidGenerator = guidGenerator;
    }

    public async Task<string> UploadAsync(string containerName, IFormFile file)
    {
        var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        await blobContainerClient.CreateIfNotExistsAsync();
        await blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);

        var fileExtension = Path.GetExtension(file.FileName);
        var fileName = $"{_guidGenerator.Generate()}{fileExtension}";

        BlobClient blobClient = blobContainerClient.GetBlobClient(fileName);
        
        var header = new BlobHttpHeaders();
        header.ContentType = file.ContentType;

        await blobClient.UploadAsync(file.OpenReadStream(), header);

        return fileName;
    }

    public async Task DeleteAsync(string containerName, string fileName)
    {
        var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        BlobClient blobClient = blobContainerClient.GetBlobClient(fileName);
        await blobClient.DeleteAsync();
    }

    public List<string> GetFiles(string containerName)
    {
        var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        return blobContainerClient.GetBlobs().Select(b => b.Name).ToList();
    }

    public bool HasFile(string containerName, string fileName)
    {
        var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        return blobContainerClient.GetBlobs().Any(b => b.Name == fileName);
    }
}
