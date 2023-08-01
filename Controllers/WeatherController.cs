using System.IO;
using Historical_Weather_Website.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

public class WeatherController : Controller
{
    private readonly WeatherForecastService _weatherForecastService;
    private readonly IWebHostEnvironment _env;

    public WeatherController(WeatherForecastService weatherForecastService, IWebHostEnvironment env) 
    {
        _weatherForecastService = weatherForecastService;
        _env = env;  
    }


    private async Task<(double Latitude, double Longitude)> GetLatLonFromCityName(string city, string state)
    {
        var locationQuery = $"{city},{state}";
        var request = new HttpRequestMessage(HttpMethod.Get,
            $"http://api.openweathermap.org/geo/1.0/direct?q={locationQuery}&limit=1&appid={_weatherForecastService.ApiKey}");

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
    public async Task<IActionResult> Index(string city, string state, string zipcode, string date, string units)
    {
        (double Latitude, double Longitude) = await GetLatLonFromCityName(city, state);

        
        WeatherForecast forecast = await _weatherForecastService.GetCurrentWeatherForecastAsync(zipcode, units);

        foreach (var weather in forecast.weather)
        {
            var imgPath = Path.Combine(_env.WebRootPath, "images", $"{weather.main}.png");
            if (!System.IO.File.Exists(imgPath))
            {
                weather.main = "Clear";
            }
        }

        return View(forecast);
    }
}

public class Location
{
    public string name { get; set; }
    public double lat { get; set; }
    public double lon { get; set; }
}




