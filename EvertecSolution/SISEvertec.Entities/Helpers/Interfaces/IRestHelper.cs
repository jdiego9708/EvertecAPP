using SISEvertec.Entities.ModelsBinding.ModelsConfiguration.ConfigurationSISEvertec;

namespace SISEvertec.Entities.Helpers.Interfaces
{
    public interface IRestHelper
    {
        Task<RestResponseModel> CallMethodGetAsync(string controller);
        Task<RestResponseModel> CallMethodPostAsync(string controller, string data, string token = "");
    }
}
