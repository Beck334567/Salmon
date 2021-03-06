using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Salmon.Models
{
    public class RequestPlace
    {
        //TODO Add Vadildation 
        public string Name { get; set; }
        public string Radius { get; set; }
        public Location Location { get; set; }
        public string Rating { get; set; }
        public decimal UserRatingsTotal { get; set; }
        public bool IsOpenNow { get; set; }

    }
    public class Location
    {
        [JsonProperty("lat")]
        public string Latitude { get; set; }
        [JsonProperty("lng")]
        public string Longitude { get; set; }
    }
}
