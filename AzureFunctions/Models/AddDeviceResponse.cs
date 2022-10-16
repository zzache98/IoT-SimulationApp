
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctions.Models
{
    public class AddDeviceResponse
    {
        public string DeviceId { get; set; }    
        public string Message { get; set; } = "Device was successfully added";
        public string DeviceConnectionString { get; set; }
    }
}
