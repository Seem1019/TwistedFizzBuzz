using System.Text.Json.Serialization;

namespace TwistedFizzBuzz.Library.ApiResponses
{
    public class ApiTokenResponse
    {
        [JsonPropertyName("multiple")]
        public int Multiple { get; set; }

        [JsonPropertyName("word")]
        public string Word { get; set; }
    }
}
