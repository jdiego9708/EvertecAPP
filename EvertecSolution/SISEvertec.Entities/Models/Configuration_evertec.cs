namespace SISEvertec.Entities.Models
{
    public class Configuration_evertec
    {
        public Configuration_evertec() 
        { 
        
        }
        public int Id { get; set; }
        public string Theme_default { get; set; }
        public string Languaje_default { get; set; }
        public DateTime Last_changed { get; set; }
    }
}
