using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetAddressManager.Client.Models
{
    public class CommonSwitch
    {
        public string IPAddress { get; set; }
        public string IPMask { get; set; }
        public string MACAddress { get; set; }
        public string? Description { get; set; }
        public string? IPGateway { get; set; }

    }
}
