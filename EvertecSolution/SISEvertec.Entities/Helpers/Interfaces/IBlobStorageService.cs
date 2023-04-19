using SISEvertec.Entities.ModelsBinding.ModelsConfiguration.ConfigurationAzure;

namespace SISEvertec.Entities.Helpers.Interfaces
{
    public interface IBlobStorageService
    {
        BlobResponse DownloadFileContainerBlobStorage(string nombreArchivo, string contenedor);
        BlobResponse UploadFileContainerBlobStorage(Stream inputStream, string nombreArchivo, string contenedor, string contentType = "image/jpeg");
    }
}
