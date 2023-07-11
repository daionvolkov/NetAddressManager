using NetAddressManager.Models;

namespace NetAddressManager.Api.Models
{
    public class AccessSwitch : CommonObject
    {
        public int? AggregationSwitchId { get; set; }
        public AggregationSwitch? AggregationSwitch { get; set; }

        public AccessSwitch()
        {
        }

        public AccessSwitch(AccessSwitchModel accessSwitchModel) : base(accessSwitchModel)
        {
            Id = accessSwitchModel.Id;
            IPAddress = accessSwitchModel.IPAddress;
            IPMask = accessSwitchModel.IPMask;
            MACAddress = accessSwitchModel.MACAddress;
            Description = accessSwitchModel.Description;
            CreateDate = accessSwitchModel.CreateDate;
            Status = accessSwitchModel.Status;
            ManufacturerId = accessSwitchModel.ManufacturerId;
            PostalAddressId = accessSwitchModel.PostalAddressId;
            SwitchPortId = accessSwitchModel.SwitchPortId;
        }

        public AccessSwitch GetModel()
        {
            return new AccessSwitch
            {
                Id = this.Id,
                IPAddress = this.IPAddress,
                IPMask = this.IPMask,
                MACAddress = this.MACAddress,
                Description = this.Description,
                CreateDate = this.CreateDate,
                Status = this.Status,
                ManufacturerId = this.ManufacturerId,
                PostalAddressId = this.PostalAddressId,
                SwitchPortId = this.SwitchPortId,
            };
        }
    }
}
