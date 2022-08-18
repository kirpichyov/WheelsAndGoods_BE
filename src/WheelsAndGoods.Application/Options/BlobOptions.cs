namespace WheelsAndGoods.Application.Options;

public class BlobOptions
{
    public string ConnectionString { get; set; }
    public BlobContainersNode ContainerNames { get; set; }
    
    public class BlobContainersNode
    {
        public string UserAvatars { get; set; }
        public string OrderPhotos { get; set; }
    }
}
