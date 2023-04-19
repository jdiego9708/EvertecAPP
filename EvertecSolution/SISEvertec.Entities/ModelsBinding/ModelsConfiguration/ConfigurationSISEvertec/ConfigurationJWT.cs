namespace SISEvertec.Entities.ModelsBinding.ModelsConfiguration.ConfigurationSISEvertec
{
    public class ConfigurationJWT
    {
        public ConfigurationJWT()
        {
            this.Issuer = string.Empty;
            this.Audience = string.Empty;
            this.SecretJWT = string.Empty;
        }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecretJWT { get; set; }
    }
}
