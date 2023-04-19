namespace SISEvertec.Entities.ModelsBinding.ModelsConfiguration.ConfigurationAzure
{
    public class BlobStorageServiceModel
    {
        public string DefaultEndpointsProtocol { get; set; }
        public string ContainerNameRecursosImagenes { get; set; }
        public string AccountName { get; set; }
        public string AccountKey { get; set; }
        public string EndpointSuffix { get; set; }
    }
}
