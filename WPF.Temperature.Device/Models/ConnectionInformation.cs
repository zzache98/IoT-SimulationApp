using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Temperature.Device.Models
{
    public class ConnectionInformation
    {
        public string ConnectionEndpoint { get; set; } = "http://localhost:7008/api/devices/connect";
    }
}
