using NetAddressManager.Api.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetAddressManager.Models
{
    public class CoreSwitchModel : CommonModel
    {
        public string IPGateway { get; set; }
        public List<int>? AggregationSwitchIds { get; set; } = new List<int>();

        public CoreSwitchModel() { }

        public CoreSwitchModel(string iPAddress, string iPMask, string mACAddress, string description,
           SwitchStatus status,  string iPGateway, List<int> switchPortIds=null, List<int> aggregationSwitchIds = null)
        {
            IPAddress = iPAddress;
            IPMask = iPMask;
            MACAddress = mACAddress;
            Description = description;
            Status = status;
            CreateDate = DateTime.Now;
            SwitchPortIds = switchPortIds;
            IPGateway = iPGateway;
            AggregationSwitchIds = aggregationSwitchIds;
        }
    }
}
