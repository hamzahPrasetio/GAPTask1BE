using System.Text.Json.Serialization;

namespace task1be.Models
{
    public class Team
    {
        [JsonPropertyName("Id")]
        public int id { get; set; }
        [JsonPropertyName("Name")]
        public string name { get; set; }
        [JsonPropertyName("Manager_Name")]
        public string managername { get; set; }
        [JsonPropertyName("Stadium_Name")]
        public string stadiumname { get; set; }
        [JsonPropertyName("Country")]
        public string country { get; set; }
        [JsonPropertyName("City")]
        public string city { get; set; }
        
        // Navigation property for the Players in the team
        [JsonIgnore]
        public ICollection<Player> players { get; set; } = new List<Player>();
    }
}
