using NetAddressManager.Api.Models.Enums;
using NetAddressManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace NetAddressManager.Client.Models
{
    public class SwitchDetailsModel<T>
    {
        public T SwitchData { get; set; }
        public string? IPGateway { get; set; }
        public SwitchType SwitchType { get; set; }
        public string? PostalAddress { get; set; }
        public string? Equipment { get; set;}
    }
}
