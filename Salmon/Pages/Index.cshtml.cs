using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Salmon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Salmon.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private const string Key = "AIzaSyA8AdwlyUEKwmtWK8K9pS3_mcyUWeSfPek";

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }

        //Index?handler=SearchDetail
        public IActionResult OnPostSearchDetail(string placeId)
        {
            //Todo : Let fields can be selected
            var basicApiUrl = $"https://maps.googleapis.com/maps/api/place/details/json?key={Key}&";
            var placeDetailApiUrl = basicApiUrl + "&" +
                                    $"place_id={placeId}&fields=name,reviews,rating,website,formatted_phone_number,geometry";
            try
            {
                var response = CallGoogleMapAsync(placeDetailApiUrl);
                var responsePlaceDetail = JsonConvert.DeserializeObject<ResponsePlaceDetail>(response.Result);
                var result = new
                {
                    responsePlaceDetail.Result.Geometry.Location.Latitude,
                    responsePlaceDetail.Result.Geometry.Location.Longitude,
                    responsePlaceDetail.Result
                };

                return new JsonResult(result);
            }
            catch (HttpRequestException exception)
            {
                Console.WriteLine("An HTTP request exception occurred. {0}", exception.Message);
            }
            return Content(placeId);
        }

        //Index?handler=SearchItem
        public IActionResult OnPostSearchItem(RequestPlace requestPlace, bool isRandomSelect)
        {
            //TODO add other's fields to select
            var basicApiUrl = $"https://maps.googleapis.com/maps/api/place/nearbysearch/json?key={Key}";
            var nearbySearchApiUrl = basicApiUrl + "&" +
                                     $"location={requestPlace.Location.Latitude},{requestPlace.Location.Longitude}&radius={requestPlace.Radius}&keyword={requestPlace.Name}&language=zh-TW";

            try
            {
                var response = CallGoogleMapAsync(nearbySearchApiUrl);
                var responsePlace = JsonConvert.DeserializeObject<ResponsePlace>(response.Result);

                var result = responsePlace.Results
                            .Where(x => (Convert.ToDecimal(x.Rating) >= Convert.ToDecimal(requestPlace.Rating)) && (Convert.ToDecimal(x.UserRatingsTotal) >= Convert.ToDecimal(requestPlace.UserRatingsTotal)))
                            .Select(x => new
                            {
                                x.Name,
                                x.Rating,
                                x.PlaceId,
                                x.UserRatingsTotal,
                                x.Vicinity
                            }).OrderByDescending(x => x.Rating)
                          .ThenByDescending(x => x.UserRatingsTotal);
                if (isRandomSelect)
                {
                    var a = responsePlace.Results.Where(x =>
                        Convert.ToDecimal(x.Rating) >= Convert.ToDecimal(requestPlace.Rating)).ToList();
                    var b = RandomSelect(a);
                    var c = new List<Result> {b};
                    return new JsonResult(c);
                }

                return new JsonResult(result);
            }
            catch (HttpRequestException exception)
            {
                Console.WriteLine("An HTTP request exception occurred. {0}", exception.Message);
            }

            return Content(nearbySearchApiUrl);
        }

        private async Task<string> CallGoogleMapAsync(string googleApi)
        {
            HttpClient myAppHTTPClient = new HttpClient();
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
            HttpResponseMessage responseMessage = await myAppHTTPClient.PostAsync(googleApi, httpRequestMessage.Content);
            HttpContent content = responseMessage.Content;
            var response = content.ReadAsStringAsync();
            return await response;
        }

        public T RandomSelect<T>(List<T> list)
        {
            Random random = new Random();
            int number = random.Next(list.Count);
            var result = list[number];
            return result;
        }
    }
}