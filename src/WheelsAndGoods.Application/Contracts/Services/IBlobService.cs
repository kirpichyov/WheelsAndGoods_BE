using WheelsAndGoods.Application.Models.Blob;

namespace WheelsAndGoods.Application.Contracts.Services;

public interface IBlobService
{
    (string Uri, Guid BlobName) CreateUploadLink(BlobContainer container);
    string CreateReadonlyLink(BlobContainer container, Guid blobName);
    Task Delete(BlobContainer container, string blobName);
}
