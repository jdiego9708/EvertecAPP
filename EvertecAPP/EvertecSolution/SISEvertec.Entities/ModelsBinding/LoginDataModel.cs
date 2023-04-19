using Newtonsoft.Json;

namespace SISEvertec.Entities.ModelsBinding
{
    public class LoginDataModel
    {
        public LoginDataModel()
        {
            this.Email = string.Empty;
            this.FirstName = string.Empty;
            this.SurName = string.Empty;
            this.AuthorizationToken = string.Empty;
        }

        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("firstname")]
        public string FirstName { get; set; }
        [JsonProperty("surname")]
        public string SurName { get; set; }
        [JsonProperty("authorizationToken")]
        public string AuthorizationToken { get; set; }
    }
}
