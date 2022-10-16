using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Temperature.Device.Models
{
    public class DeviceInformation
    {
        public string DeviceId { get; set; } = Guid.NewGuid().ToString();
        public string DeviceType { get; set; } = "thermometer";
        public string DeviceName { get; set; } = "";
        public string Location { get; set; } = "";
        public string ConnectionString { get; set; } = "";
        public bool IsConfigured { get; set; } = false;
    }
}
