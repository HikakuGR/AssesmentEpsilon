using AssesmentEpsilon;
using BlazorApp.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
        const int pageSize = 5;
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly DatabaseContext _databaseContext;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,DatabaseContext databaseContext )
        {
            _logger = logger;
            _databaseContext = databaseContext;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get(int page)
        {
            _databaseContext.Customers.Add(new Customer { Address="asd", ContactName="qwe"});
            await _databaseContext.SaveChangesAsync();
            return Enumerable.Range(page * pageSize - 4, pageSize).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}