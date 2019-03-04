using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace NA.Template.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LineChartController : ControllerBase
    {
        [HttpGet("[action]")]
        public WeatherForecast GetWeatherForecast()
        {
            var weatherList = new List<Weather>();
            weatherList.Add(new Weather { Label = "New Delhi", Data = new int[] { 6, 19, 6, 21, 7, 15 } });
            weatherList.Add(new Weather { Label = "New York", Data = new int[] { -8, -6, -1, 2, -7, 6 } });
            weatherList.Add(new Weather { Label = "Moscow", Data = new int[] { -4, 3, -5, -1, -6, -3 } });
            weatherList.Add(new Weather { Label = "London", Data = new int[] { 6, 2, 4, 6, 7, 7 } });

            List<string> lineChartLabelsList = new List<string>();

            for (int i = 0; i < 3; i++)
            {
                lineChartLabelsList.AddRange(new string[] { $"{(DayOfWeek)i} High", $"{(DayOfWeek)i} Low" });
            }
            return new WeatherForecast { WeatherList = weatherList, ChartLabels = lineChartLabelsList.ToArray() };
        }

        public class Weather
        {
            public int[] Data { get; set; }
            public string Label { get; set; }
        }

        public class WeatherForecast
        {
            public List<Weather> WeatherList { get; set; }
            public string[] ChartLabels { get; set; }
        }
    }
}