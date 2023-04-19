using Microsoft.Extensions.Configuration;
using SISEvertec.Entities.Helpers.Interfaces;
using SISEvertec.Entities.ModelsBinding.ModelsConfiguration.ConfigurationSISEvertec;
using RestSharp;
using RestSharp.Authenticators;
using Azure;
using System.Text;

namespace SISEvertec.Entities.Helpers
{
    public class RestHelper : IRestHelper
    {
        private readonly ConfigurationEvertec ConfigurationEvertec;
        public RestHelper(IConfiguration IConfiguration)
        {
            var settings = IConfiguration.GetSection("ConfigurationEvertec");
            this.ConfigurationEvertec = settings.Get<ConfigurationEvertec>();
        }
        public async Task<RestResponseModel> CallMethodPostAsync(string controller, string data, string token = "")
        {
            try
            {
                string apiUrl;

                bool isProduccion = this.ConfigurationEvertec.IsProduction;

                if (isProduccion)
                    apiUrl = this.ConfigurationEvertec.URLApiProduction;
                else
                    apiUrl = this.ConfigurationEvertec.URLApiDevelopment;

                string url = $"{apiUrl}{controller}";

                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                RestClient client = new(url);

                if (!string.IsNullOrEmpty(token))
                    client.Authenticator = new JwtAuthenticator(token);

                RestRequest request = new()
                {
                    Method = Method.Post
                };

                request.AddHeader("Content-Type", "application/json");

                if (!string.IsNullOrEmpty(data))
                    request.AddJsonBody(data);

                RestResponse result = await client.ExecuteAsync(request);

                if (result == null)
                    throw new Exception("Error llamando al servidor");

                string content = result.Content.ToString();

                if (string.IsNullOrEmpty(content))
                    throw new Exception("Error con el contenido de la respuesta");

                if (!result.IsSuccessful)
                    throw new Exception(content);

                if (result.IsSuccessful)
                {
                    return new RestResponseModel
                    {
                        Success = true,
                        Message = result.Content,
                    };
                }
                else
                {
                    return new RestResponseModel
                    {
                        Success = false,
                        Message = result.Content,
                    };
                }
            }
            catch (Exception ex)
            {
                return new RestResponseModel
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<RestResponseModel> CallMethodGetAsync(string controller)
        {
            try
            {
                string apiUrl;

                bool isProduccion = this.ConfigurationEvertec.IsProduction;

                if (isProduccion)
                    apiUrl = this.ConfigurationEvertec.URLApiProduction;
                else
                    apiUrl = this.ConfigurationEvertec.URLApiDevelopment;

                string url = $"{apiUrl}{controller}";

                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                RestClient client = new(url);

                RestRequest request = new()
                {
                    Method = Method.Get
                };

                //request.AddHeader("Content-Type", "application/json");

                RestResponse result = await client.ExecuteAsync(request);

                if (result == null)
                    throw new Exception("Error llamando al servidor");

                string content = result.Content.ToString();

                if (string.IsNullOrEmpty(content))
                    throw new Exception("Error con el contenido de la respuesta");

                if (!result.IsSuccessful)
                    throw new Exception(content);

                if (result.IsSuccessful)
                {
                    return new RestResponseModel
                    {
                        Success = true,
                        Message = result.Content,
                    };
                }
                else
                {
                    return new RestResponseModel
                    {
                        Success = false,
                        Message = result.Content,
                    };
                }
            }
            catch (Exception ex)
            {
                return new RestResponseModel
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
