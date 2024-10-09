using System.Text.Json.Serialization;

namespace task1be.Models
{
    public class Position
    {
        [JsonPropertyName("Id")]
        public int id { get; set; }
        [JsonPropertyName("Name")]
        public string name { get; set; }
        
        // Navigation property for the Players related to this position
        [JsonIgnore]
        public ICollection<Player> players { get; set; } = new List<Player>();
    }
}
