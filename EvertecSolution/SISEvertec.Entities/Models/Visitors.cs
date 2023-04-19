namespace SISEvertec.Entities.Models
{
    public class Visitors
    {
        public Visitors()
        {
            this.Name_visitor = string.Empty;
            this.Image_visitor = string.Empty;
        }
        public int Id { get; set; }
        public string Name_visitor { get; set; }
        public string Image_visitor { get; set; }
        public DateTime Date_visitor { get; set; }
        public TimeSpan Hour_visitor { get; set; }
    }
}
