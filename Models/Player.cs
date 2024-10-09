using System.Text.Json.Serialization;

namespace task1be.Models
{
    public class Player
    {
        [JsonPropertyName("Id")]
        public int id { get; set; }
        [JsonPropertyName("Shirt_No")]
        public int? shirtno { get; set; }  // Nullable if shirt number is optional
        [JsonPropertyName("Name")]
        public string name { get; set; }
        [JsonPropertyName("Position_Id")]
        public int positionid { get; set; }  // Foreign key for Position
        [JsonPropertyName("Team_Id")]
        public int teamid { get; set; }  // Foreign key for Team
        [JsonPropertyName("Appearances")]
        public int? appearances { get; set; }
        [JsonPropertyName("Goals")]
        public int? goals { get; set; }
        [JsonPropertyName("Assist")]
        public int? assist { get; set; }
        [JsonPropertyName("Yellow_Card")]
        public int? yellowcard { get; set; }
        [JsonPropertyName("Red_Card")]
        public int? redcard { get; set; }

        // Navigation properties for relationships
        [JsonIgnore]
        public Position? position { get; set; }
        [JsonIgnore]
        public Team? team { get; set; }
    }
}
