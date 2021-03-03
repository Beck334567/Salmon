using Newtonsoft.Json;
using System.Collections.Generic;

namespace Salmon.Models
{
    public class ResponsePlace
    {
        [JsonProperty("business_status")]
        public string BusinessStatus { get; set; }

        [JsonProperty("next_page_token")]
        public string NextPageToken { get; set; }

        [JsonProperty("results")]
        public List<Result> Results { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

    }

    public class Result
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("place_id")]
        public string PlaceId { get; set; }

        [JsonProperty("rating")]
        public string Rating { get; set; }

        [JsonProperty("user_ratings_total")]
        public string UserRatingsTotal { get; set; }

        [JsonProperty("opening_hours")]
        public Opening_hour OpeningHours { get; set; }

        [JsonProperty("vicinity")]
        public string Vicinity { get; set; }

        [JsonProperty("types")]
        public List<string> PlaceTypes { get; set; }

    }

    public class Opening_hour
    {
        [JsonProperty("open_now")]
        public bool OpenNow { get; set; }
    }
}