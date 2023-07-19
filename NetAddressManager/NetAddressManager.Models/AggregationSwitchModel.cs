using NetAddressManager.Api.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetAddressManager.Models
{
    public class AggregationSwitchModel : CommonModel
    {
        public int? CoreSwitchId { get; set; }
        public List<int> AccessSwitchIds { get; set; } = new List<int>();
        public AggregationSwitchModel() { }



        public AggregationSwitchModel(string iPAddress, string iPMask, string mACAddress, string description, SwitchStatus status)
        {
            IPAddress = iPAddress;
            IPMask = iPMask;
            MACAddress = mACAddress;
            Description = description;
            Status = status;
            CreateDate = DateTime.Now;
        }
    }
}
