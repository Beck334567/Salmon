using Newtonsoft.Json;
using System.Collections.Generic;

namespace Salmon.Models
{
    public class ResponseDistance
    {
        [JsonProperty("destination_addresses")]
        public List<string> DestinationAddresses { get; set; }

        [JsonProperty("origin_addresses")]
        public List<string> OriginAddresses { get; set; }

        [JsonProperty("rows")]
        public List<DistanceMatrix> Result { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class DistanceMatrix
    {
        [JsonProperty("elements")]
        public List<Element> Element { get; set; }
    }

    public class Element
    {
        [JsonProperty("distance")]
        public Distance Distance { get; set; }

        [JsonProperty("duration")]
        public Duration Duration { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class Distance
    {
        [JsonProperty("text")]
        public string Text { get; set; }
        //Meter
        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class Duration
    {
        [JsonProperty("text")]
        public string Text { get; set; }
        //Second
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}