using NetAddressManager.Models;

namespace NetAddressManager.Api.Models
{
    public class AggregationSwitch : CommonObject
    {
        public int? CoreSwitchId { get; set; }
        public CoreSwitch? CoreSwitch { get; set; }
        public List<AccessSwitch> AccessSwitches { get; set; } = new List<AccessSwitch>();

        public AggregationSwitch()
        {
        }

        public AggregationSwitch(AggregationSwitchModel aggregationSwitchModel) : base(aggregationSwitchModel)
        {
            Id = aggregationSwitchModel.Id;
            IPAddress= aggregationSwitchModel.IPAddress;
            IPMask= aggregationSwitchModel.IPMask;
            MACAddress= aggregationSwitchModel.MACAddress;
            Description = aggregationSwitchModel.Description;
            CreateDate = DateTime.Now;
            Status = aggregationSwitchModel.Status;
            EquipmentManufacturerId = aggregationSwitchModel.EquipmentManufacturerId;
            PostalAddressId = aggregationSwitchModel.PostalAddressId;
            SwitchPortId = aggregationSwitchModel.SwitchPortId;
            CoreSwitchId = aggregationSwitchModel.CoreSwitchId;
        }

        public AggregationSwitchModel GetModel()
        {
            return new AggregationSwitchModel
            {
                Id=this.Id, 
                IPAddress=this.IPAddress,
                IPMask = this.IPMask, 
                MACAddress=this.MACAddress,
                Description =this.Description,
                CreateDate = this.CreateDate,
                Status = this.Status,
                EquipmentManufacturerId = this.EquipmentManufacturerId,
                PostalAddressId = this.PostalAddressId,
                SwitchPortId = this.SwitchPortId,
                CoreSwitchId = this.CoreSwitchId,
            };
        }

    }

}
