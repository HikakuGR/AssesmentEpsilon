using Azure;
using BlazorApp.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace AssesmentEpsilon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BingController : ControllerBase
    {
        private readonly IHttpClientFactory httpClientFactory;

        public BingController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<BingResponse> SearchBing(string input)
        {
            var httpClient = httpClientFactory.CreateClient();
            return await httpClient.GetFromJsonAsync<BingResponse>($"https://dev.virtualearth.net/REST/v1/Autosuggest?query={input}&userLocation=40.629269,22.947412,5&includeEntityTypes=Business&key=AvfSq16aQ89oaTYEHQxPlShheEVgpFGuSeTpz2xYKQScHMb8Xvn4aba0Wr8bUFWl");
            
        }
    }
}
