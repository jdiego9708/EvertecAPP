using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using SISEvertec.Entities.ModelsBinding.ModelsConfiguration.ConfigurationAzure;

namespace SISEvertec.Entities.Helpers
{
    public class BlobStorageService
    {
        private readonly BlobStorageServiceModel BlobStorageServiceModel;
        public BlobStorageService(IConfiguration IConfiguration)
        {
            var settings = IConfiguration.GetSection("BlobStorageService");
            this.BlobStorageServiceModel = settings.Get<BlobStorageServiceModel>();
        }
        private BlobContainerClient ConfiguracionContenedor(string nombreContenedor)
        {
            string defaultEndpointsProtocol = this.BlobStorageServiceModel.DefaultEndpointsProtocol;
            string accountName = this.BlobStorageServiceModel.AccountName;
            string accountKey = this.BlobStorageServiceModel.AccountKey;
            string endPointSuffix = this.BlobStorageServiceModel.EndpointSuffix;
            string connectionString = $"DefaultEndpointsProtocol={defaultEndpointsProtocol};AccountName={accountName};AccountKey={accountKey};EndpointSuffix={endPointSuffix}";

            return new BlobContainerClient(connectionString, nombreContenedor);
        }
        public BlobResponse DownloadFileContainerBlobStorage(string nombreArchivo, string contenedor)
        {
            BlobResponse blobResponse = new();
            try
            {
                BlobContainerClient clientContainer = ConfiguracionContenedor(contenedor);
                clientContainer.CreateIfNotExists(PublicAccessType.Blob);

                BlockBlobClient blockBlob = clientContainer.GetBlockBlobClient(nombreArchivo);
                Stream mem = new MemoryStream();
                if (blockBlob != null)
                {
                    blockBlob.DownloadTo(mem);
                }
                byte[] archivo = ((MemoryStream)mem).ToArray();
                blobResponse.IsSuccess = true;
                blobResponse.Message = archivo;
            }
            catch (Exception ex)
            {
                blobResponse.IsSuccess = false;
                blobResponse.Message = ex.Message;
            }
            return blobResponse;
        }
        public BlobResponse UploadFileContainerBlobStorage(Stream inputStream, string nombreArchivo, string contenedor, string contentType = "image/jpeg")
        {
            BlobResponse blobResponse = new();
            try
            {
                BlobContainerClient clientContainer = ConfiguracionContenedor(contenedor);
                clientContainer.CreateIfNotExists(PublicAccessType.Blob);

                BlockBlobClient blockBlob = clientContainer.GetBlockBlobClient(nombreArchivo);

                BlobHttpHeaders headers = new() { ContentType = contentType };

                blockBlob.Upload(inputStream, new BlobUploadOptions { HttpHeaders = headers });

                blobResponse.IsSuccess = true;
                blobResponse.Message = blockBlob.Uri.AbsoluteUri;
            }
            catch (Exception ex)
            {
                blobResponse.IsSuccess = false;
                blobResponse.Message = ex.Message;
            }
            return blobResponse;
        }
    }
}
