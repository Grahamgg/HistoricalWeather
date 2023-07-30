using Newtonsoft.Json;
using System.Collections.Generic;

namespace Historical_Weather_Website.Models
{
    public class WeatherForecast
    {
        public List<Weather> weather { get; set; }
        public Main main { get; set; }
        public string name { get; set; }
        
        public Sys sys { get; set; }
    }

    public class Weather
    {
        public string main { get; set; }
    }

    public class Main
    {
        public float temp { get; set; }
    }
    public class Sys
    {
        public string country { get; set; }
    }

}



