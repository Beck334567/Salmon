using Newtonsoft.Json;
using System.Collections.Generic;

namespace Salmon.Models
{
    public class ResponsePlaceDetail
    {
        [JsonProperty("result")]
        public PlaceDetailResult Result { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class PlaceDetailResult
    {
        [JsonProperty("formatted_phone_number")]
        public string PhoneNumber { get; set; }

        [JsonProperty("geometry")]
        public Geometry Geometry { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("rating")]
        public string Rating { get; set; }

        [JsonProperty("reviews")]
        public List<Review> Reviews { get; set; }

        [JsonProperty("website")]
        public string Website { get; set; }

        [JsonProperty("types")]
        public List<string> Types { get; set; }

        [JsonProperty("business_status")]
        public string BusinessStatus { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("address_components")]
        public string AddressComponents { get; set; }

        [JsonProperty("formatted_address")]
        public string Address { get; set; }

        [JsonProperty("adr_address")]
        public string AdrAddress { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("opening_hours")]
        public OpeningHour OpeningHours { get; set; }

        [JsonProperty("place_id")]
        public string PlaceId { get; set; }

        [JsonProperty("vicinity")]
        public string Vicinity { get; set; }
    }

    public class Geometry
    {
        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("viewport")]
        public object Viewport { get; set; }
    }

    public class Review
    {
        [JsonProperty("author_name")]
        public string AuthorName { get; set; }

        [JsonProperty("author_url")]
        public string AuthorUrl { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("profile_photo_url")]
        public string ProfilePhotoUrl { get; set; }

        [JsonProperty("rating")]
        public decimal Rating { get; set; }

        [JsonProperty("relative_time_description")]
        public string RelativeTimeDescription { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("time")]
        public decimal Time { get; set; }
    }

    public class AddressComponent
    {
        [JsonProperty("long_name")]
        public string LongName { get; set; }

        [JsonProperty("short_name")]
        public string ShortName { get; set; }

        [JsonProperty("types")]
        public List<string> Types { get; set; }
    }
}