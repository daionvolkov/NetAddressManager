namespace NetAddressManager.Api.Models
{
    public class SwitchData
    {
        public List<CoreSwitch> CoreSwitchData { get; set; } = new List<CoreSwitch>();
        public List<AggregationSwitch> AggregationSwitchData { get; set; } = new List<AggregationSwitch>();
        public List<AccessSwitch> AccessSwitchData { get; set; } = new List<AccessSwitch>();
        public List<EquipmentManufacturer> EquipmentManufacturers { get; set; } = new List<EquipmentManufacturer>();
        public List<PostalAddress> PostalAddresses { get; set; } = new List<PostalAddress>();
    }
}
