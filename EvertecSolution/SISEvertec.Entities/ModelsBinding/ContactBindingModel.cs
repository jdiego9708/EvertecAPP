using Newtonsoft.Json;

namespace SISEvertec.Entities.ModelsBinding
{
    public class ContactBindingModel
    {

        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("info")]
        public string Info { get; set; }
        [JsonProperty("avatar")]
        public string Avatar { get; set; }
    }
}
