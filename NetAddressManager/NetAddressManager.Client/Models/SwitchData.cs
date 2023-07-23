using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetAddressManager.Client.Models
{
    public class SwitchData
    {
        // Properties for Core Switch
        public int CoreSwitchId { get; set; }
        public string CoreSwitchIPAddress { get; set; }
        public string CoreSwitchIPMask { get; set; }
        public string? CoreSwitchDescription { get; set; }

        // Properties for Aggregation Switch
        public int AggregationSwitchId { get; set; }
        public string AggregationSwitchIPAddress { get; set; }
        public string AggregationSwitchIPMask { get; set; }
        public string? AggregationSwitchDescription { get; set; }

        // Properties for Access Switch
        public int AccessSwitchId { get; set; }
        public string AccessSwitchIPAddress { get; set; }
        public string AccessSwitchIPMask { get; set; }
        public string? AccessSwitchDescription { get; set; }
    }
}
