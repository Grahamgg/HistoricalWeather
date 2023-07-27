using Historical_Weather_Website.Models;
using Newtonsoft.Json;

public class WeatherForecastService
{
    public HttpClient Client { get; }
    public string ApiKey { get; }

    public WeatherForecastService(HttpClient httpClient, IConfiguration configuration)
    {
        Client = httpClient;
        ApiKey = configuration["OpenWeatherMapApiKey"];
    }

    public async Task<WeatherForecast> GetWeatherForecastAsync(string city, string units)
    {
        var response = await Client.GetAsync($"https://api.openweathermap.org/data/2.5/weather?q={city}&units={units}&appid={ApiKey}");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var forecast = JsonConvert.DeserializeObject<WeatherForecast>(content);

        return forecast;
    }
}
