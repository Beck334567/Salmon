using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Salmon.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Salmon.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private static readonly string _key = "AIzaSyA8AdwlyUEKwmtWK8K9pS3_mcyUWeSfPek";
        private static HttpClient myAppHTTPClient = new HttpClient();

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }

        //Index?handler=SearchItem
        public async Task<IActionResult> OnPostSearchItemAsync(RequestPlace requestPlace)
        {
            //TODO add other's fields to select
            var basicApiUrl = string.Format("https://maps.googleapis.com/maps/api/place/nearbysearch/json?key={0}", _key);
            var nearbySearchApiUrl = basicApiUrl + "&" + string.Format("location={0},{1}&radius={2}&keyword={3}", requestPlace.Location.Latitude, requestPlace.Location.Longitude, requestPlace.Radius, requestPlace.Name);

            
            try
            {
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
                HttpResponseMessage responseMessage = await myAppHTTPClient.PostAsync(nearbySearchApiUrl, httpRequestMessage.Content);
                HttpContent content = responseMessage.Content;
                var response = await content.ReadAsStringAsync();


                var responsePlace = JsonConvert.DeserializeObject<ResponsePlace>(response);
                var searchResult = "";

                foreach (var item in responsePlace.Results)
                {
                    if (Convert.ToDecimal(item.Rating) >= Convert.ToDecimal(requestPlace.Rating))
                    {
                        searchResult += "店名" + item.Name + "分數" + item.Rating;
                    }
                }
                return Content(searchResult);
            }
            catch (HttpRequestException exception)
            {
                Console.WriteLine("An HTTP request exception occurred. {0}", exception.Message);
            }



            return Content(nearbySearchApiUrl);
        }
    }
}