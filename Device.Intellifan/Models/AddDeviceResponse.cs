using Microsoft.Azure.Devices.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Device.Intellifan.Models
{
    internal class AddDeviceResponse
    {
        public string DeviceId { get; set; }
        public string Message { get; set; }
        public string DeviceConnectionString { get; set; }
    }
}
