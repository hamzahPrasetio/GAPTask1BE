using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace task1be.DTOs
{
    public class ResponseLovDataDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class ResponseLovDto
    {
        [JsonPropertyName("total")]
        public int Total { get; set; }

        [JsonPropertyName("data")]
        public List<ResponseLovDataDto> Data { get; set; } = new List<ResponseLovDataDto>();

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}
