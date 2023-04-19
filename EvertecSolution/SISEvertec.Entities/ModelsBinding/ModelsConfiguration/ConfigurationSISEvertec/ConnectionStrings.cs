namespace SISEvertec.Entities.ModelsBinding.ModelsConfiguration.ConfigurationSISEvertec
{
    public class ConnectionStrings
    {
        public ConnectionStrings()
        {
            this.SQLiteConnection = string.Empty;
            this.ConnectionBDDevelopment = string.Empty;
            this.ConnectionBDProduction = string.Empty;
        }
        public string SQLiteConnection { get; set; }
        public string ConnectionBDDevelopment { get; set; }
        public string ConnectionBDProduction { get; set; }
    }
}
