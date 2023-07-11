using NetAddressManager.Api.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetAddressManager.Models
{
    public class AccessSwitchModel : CommonModel
    {
        public int? AggregationSwitchId { get; set; }

        public AccessSwitchModel()
        {
        }

        public AccessSwitchModel(string iPAddress, string iPMask, string mACAddress, string description,
           SwitchStatus status,  List<int> switchPortIds = null)
        {
            IPAddress = iPAddress;
            IPMask = iPMask;
            MACAddress = mACAddress;
            Description = description;
            Status = status;
            CreateDate = DateTime.Now;
            SwitchPortIds = switchPortIds;
        }

    }

}
