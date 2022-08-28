using System.Text.Json.Serialization;

namespace NowwaitingSpotSearch.Models
{
    public class OAuthResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("scope")]
        public string Scope { get; set; }

        [JsonPropertyName("team_name")]
        public string TeamName { get; set; }

        [JsonPropertyName("team_id")]
        public string TeamId { get; set; }

        [JsonPropertyName("enterprise_id")]
        public string EnterpriseId { get; set; }
    }
}
