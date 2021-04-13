using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Salmon.Models
{
    public class RequestDistance
    {
        //TODO Add Vadildation 

        [JsonProperty("origins")]
        public Location Origins { get; set; }
        [JsonProperty("destinations")]
        public Location Destinations { get; set; }
        [JsonProperty("mode")]
        public string Mode { get; set; }
    }
    
}
