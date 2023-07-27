namespace Historical_Weather_Website.Models
{
    public class WeatherForecast
    {
        public Main main { get; set; }
        public string name { get; set; }
        public string City { get; set; }
        public string Date { get; set; }
        public float TemperatureF => main.temp * 9 / 5 - 459.67f; // Converts Kelvin to Fahrenheit.
    }

    public class Main
    {
        public float temp { get; set; } // Temperature in Kelvin.
    }
}
