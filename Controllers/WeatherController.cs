using Historical_Weather_Website.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

public class WeatherController : Controller
{
    private readonly WeatherForecastService _weatherForecastService;

    public WeatherController(WeatherForecastService weatherForecastService)
    {
        _weatherForecastService = weatherForecastService;
    }

    private async Task<(double Latitude, double Longitude)> GetLatLonFromCityName(string city)
    {
        var request = new HttpRequestMessage(HttpMethod.Get,
            $"http://api.openweathermap.org/geo/1.0/direct?q={city}&limit=1&appid={_weatherForecastService.ApiKey}");

        var client = _weatherForecastService.Client;
        var response = await client.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            var responseStream = await response.Content.ReadAsStringAsync();
            var location = JsonConvert.DeserializeObject<List<Location>>(responseStream);
            if (location != null && location.Count > 0)
            {
                return (location[0].lat, location[0].lon);
            }
        }

        throw new Exception("Unable to fetch coordinates for given city name.");
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(string city, string date, string units)
    {
        (double Latitude, double Longitude) = await GetLatLonFromCityName(city);

        WeatherForecast forecast = await _weatherForecastService.GetWeatherForecastAsync(city, units);

        return View(forecast);
    }
}

public class Location
{
    public string name { get; set; }
    public double lat { get; set; }
    public double lon { get; set; }
}


