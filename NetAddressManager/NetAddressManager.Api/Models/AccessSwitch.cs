using NetAddressManager.Models;

namespace NetAddressManager.Api.Models
{
    public class AccessSwitch : CommonObject
    {
        public int? AggregationSwitchId { get; set; }
        public AggregationSwitch? AggregationSwitch { get; set; }

        public AccessSwitch() { }

        public AccessSwitch(AccessSwitchModel accessSwitchModel) : base(accessSwitchModel)
        {
            Id = accessSwitchModel.Id;
            IPAddress = accessSwitchModel.IPAddress;
            IPMask = accessSwitchModel.IPMask;
            MACAddress = accessSwitchModel.MACAddress;
            Description = accessSwitchModel.Description;
            CreateDate = DateTime.Now;
            Status = accessSwitchModel.Status;
            EquipmentManufacturerId = accessSwitchModel.EquipmentManufacturerId;
            PostalAddressId = accessSwitchModel.PostalAddressId;
            SwitchPortId = accessSwitchModel.SwitchPortId;
        }

        public AccessSwitchModel GetModel()
        {
            return new AccessSwitchModel
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
                AggregationSwitchId = this.AggregationSwitchId
            };
        }
    }
}
