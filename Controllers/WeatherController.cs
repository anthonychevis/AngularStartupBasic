using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;


//a30bd294317ae3534e3f7c6612021806

namespace AngularSample1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class WeatherController : Controller
    {

        private readonly IOptions<ApplicationOptions> _options;

        public WeatherController(IOptions<ApplicationOptions> options)
        {
            _options = options;

        }

        [HttpGet("[action]/{city}")]
        public async Task<IActionResult> City(string city)
        {
            using (var client = new HttpClient()) {
                try {
                    client.BaseAddress = new Uri("http://api.openweathermap.org");
                    var response = await client.GetAsync($"/data/2.5/weather?q={city}&appid={_options.Value.openWeatherApiKey}&units=metric");
                    response.EnsureSuccessStatusCode();

                    string result = await response.Content.ReadAsStringAsync();
                    var rawWeather = JsonConvert.DeserializeObject<OpenWeatherResponse>(result);
                    return Ok(new {
                        Temp = rawWeather.Main.Temp,
                        Summary = string.Join(",", rawWeather.Weather.Select(x => x.Main)),
                        City = rawWeather.Name
                    });
                } catch (HttpRequestException ex) {
                    return BadRequest($"Error getting weather from OpenWeather: {ex.Message}");
                }
            }
        }

    }

    // json classes
    public class OpenWeatherResponse
    {
        public string Name { get; set; }

        public IEnumerable<WeatherDescription> Weather { get; set; }

        public Main Main { get; set; }
    }

    public class WeatherDescription
    {
        public string Main { get; set; }
        public string Description { get; set; }
    }

    public class Main
    {
        public string Temp { get; set; }
    }



}
