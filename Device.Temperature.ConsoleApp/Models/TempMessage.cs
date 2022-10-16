using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Device.Temperature.ConsoleApp.Models
{
    internal class TempMessage
    {
        [JsonProperty("main.temp")]
        public float Temperature { get; set; }

        [JsonProperty("main.humidity")]
        public int Humidity { get; set; }

        [JsonProperty("weather[0].main")]
        public string SkyCondition { get; set; } = "";
    }
}
