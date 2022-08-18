using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Microsoft.Extensions.Options;
using WheelsAndGoods.Application.Contracts.Services;
using WheelsAndGoods.Application.Models.Blob;
using WheelsAndGoods.Application.Options;

namespace WheelsAndGoods.Application.Services;

public class BlobService : IBlobService
{
    private static readonly DateTimeOffset DefaultSasExpiresOn = new(new DateTime(9999, 1, 1));
    private const int DefaultUploadLinkLifetime = 180;

    private readonly BlobServiceClient _blobServiceClient;
    private readonly BlobOptions _blobOptions;

    public BlobService(IOptions<BlobOptions> blobOptions)
    {
        _blobOptions = blobOptions.Value;
        _blobServiceClient = new BlobServiceClient(_blobOptions.ConnectionString);
    }

    public (string Uri, Guid BlobName) CreateUploadLink(BlobContainer container)
    {
        string containerName = MapToContainerName(container);
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

        var blobName = Guid.NewGuid();
        var blob = containerClient.GetBlobClient(blobName.ToString());

        var expiresOnUtc = DateTimeOffset.UtcNow.AddSeconds(DefaultUploadLinkLifetime);
        var sasUri = blob.GenerateSasUri(BlobSasPermissions.Write, expiresOnUtc);

        return (sasUri.ToString(), blobName);
    }

    public string CreateReadonlyLink(BlobContainer container, Guid blobName)
    {
        string containerName = MapToContainerName(container);
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

        var blob = containerClient.GetBlobClient(blobName.ToString());
        
        return blob.GenerateSasUri(BlobSasPermissions.Read, DefaultSasExpiresOn).ToString();
    }
    
    public async Task Delete(BlobContainer container, string blobName)
    {
        var containerName = MapToContainerName(container);
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        await containerClient.GetBlobClient(blobName).DeleteAsync();
    }
    
    private string MapToContainerName(BlobContainer container)
    {
        return container switch
        {
            BlobContainer.UserAvatars => _blobOptions.ContainerNames.UserAvatars,
            BlobContainer.OrderPhotos => _blobOptions.ContainerNames.OrderPhotos,
            _ => throw new ArgumentException($"Value '{container}' is unexpected.", nameof(container))
        };
    }
}
