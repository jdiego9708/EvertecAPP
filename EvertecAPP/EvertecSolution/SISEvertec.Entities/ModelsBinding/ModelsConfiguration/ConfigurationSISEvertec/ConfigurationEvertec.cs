namespace SISEvertec.Entities.ModelsBinding.ModelsConfiguration.ConfigurationSISEvertec
{
    public class ConfigurationEvertec
    {
        public ConfigurationEvertec()
        {
            this.Secret  = string.Empty;
            this.URLApiDevelopment = string.Empty;
            this.URLApiLocal = string.Empty;
            this.URLApiProduction = string.Empty;
        }
        public string Secret { get; set; }
        public bool IsProduction { get; set; }
        public string URLApiDevelopment { get; set; }
        public string URLApiLocal { get; set; }
        public string URLApiProduction { get; set; }
    }
}
