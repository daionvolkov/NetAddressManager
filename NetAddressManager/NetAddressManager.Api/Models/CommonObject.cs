using NetAddressManager.Api.Models.Enums;

namespace NetAddressManager.Api.Models
{
    public class CommonObject
    {
        public int Id { get; set; }
        public string IPAddress { get; set; }
        public string IPMask { get; set; }
        public string? MACAddress { get; set; }
        public string? Description { get; set; }
        public DateTime CreateDate { get; set; }
        public SwitchStatus Status { get; set; }
        public int? ManufacturerId { get; set; }
        public EquipmentManufacturer? EquipmentManufacturer { get; set; }

        public int? PostalAddressId;
        public PostalAddress? PostalAddress { get; set; }
        public int? SwitchPortId { get; set; }
        public List<SwitchPort> SwitchPorts { get; set; } = new List<SwitchPort>();




    }
}
