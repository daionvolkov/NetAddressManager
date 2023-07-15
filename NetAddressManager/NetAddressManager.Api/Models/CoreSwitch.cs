using NetAddressManager.Models;

namespace NetAddressManager.Api.Models
{
    public class CoreSwitch : CommonObject
    {
        public string IPGateway { get; set; }
        public List<AggregationSwitch> AggregationSwitches { get; set; } = new List<AggregationSwitch>();
        public CoreSwitch() { }

        public CoreSwitch(CoreSwitchModel coreSwitchModel) : base(coreSwitchModel) 
        {
            Id = coreSwitchModel.Id;
            IPAddress = coreSwitchModel.IPAddress;
            IPMask = coreSwitchModel.IPMask;
            MACAddress = coreSwitchModel.MACAddress;
            Description = coreSwitchModel.Description;
            CreateDate = DateTime.Now;
            Status = coreSwitchModel.Status;
            EquipmentManufacturerId = coreSwitchModel.EquipmentManufacturerId;
            PostalAddressId = coreSwitchModel.PostalAddressId;
            SwitchPortId = coreSwitchModel.SwitchPortId;
            IPGateway = coreSwitchModel.IPGateway;
        }

        public CoreSwitchModel GetModel()
        {
            return new CoreSwitchModel
            {
                Id = this.Id,
                IPAddress = this.IPAddress,
                IPMask = this.IPMask,
                MACAddress = this.MACAddress,
                Description = this.Description,
                CreateDate = this.CreateDate,
                Status = this.Status,
                EquipmentManufacturerId = this.EquipmentManufacturerId,
                PostalAddressId = this.PostalAddressId,
                SwitchPortId = this.SwitchPortId,
                IPGateway = this.IPGateway,
            };
        }
    }
}
