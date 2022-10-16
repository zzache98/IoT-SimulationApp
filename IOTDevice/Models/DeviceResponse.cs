using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOTDevice.Models
{
    public class DeviceResponse
    {
        public string DeviceId { get; set; }
        public string Message { get; set; }
        public string DeviceConnectionString { get; set; }
    }
}
