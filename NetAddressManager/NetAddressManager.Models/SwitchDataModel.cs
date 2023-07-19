using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetAddressManager.Models
{
    public class SwitchDataModel
    {
        public List<CoreSwitchModel> CoreSwitchData { get; set; } = new List<CoreSwitchModel>();
        public List<AggregationSwitchModel> AggregationSwitchData { get; set; } = new List<AggregationSwitchModel>();
        public List<AccessSwitchModel> AccessSwitchData { get; set; } = new List<AccessSwitchModel>();
        public List<EquipmentManufacturerModel> EquipmentManufacturers { get; set; } = new List<EquipmentManufacturerModel>();
        public List<PostalAddressModel> PostalAddresses { get; set; } = new List<PostalAddressModel>();

    }
}
