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
           SwitchStatus status, string iPGateway, int postalAddressId = 0, int equipmentManufacturerId = 0, List<int> switchPortIds=null, List<int> aggregationSwitchIds = null)
        {
            IPAddress = iPAddress;
            IPMask = iPMask;
            MACAddress = mACAddress;
            Description = description;
            Status = status;
            IPGateway = iPGateway;
            PostalAddressId = postalAddressId;
            EquipmentManufacturerId = equipmentManufacturerId;
            CreateDate = DateTime.Now;
            SwitchPortIds = switchPortIds;
            AggregationSwitchIds = aggregationSwitchIds;
        }
    }
}
