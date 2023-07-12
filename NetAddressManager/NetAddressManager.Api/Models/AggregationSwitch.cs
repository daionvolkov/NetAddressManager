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
            CreateDate = aggregationSwitchModel.CreateDate;
            Status = aggregationSwitchModel.Status;
            ManufacturerId = aggregationSwitchModel.ManufacturerId;
            PostalAddressId = aggregationSwitchModel.PostalAddressId;
            SwitchPortId = aggregationSwitchModel.SwitchPortId;
            CoreSwitchId = aggregationSwitchModel.CoreSwitchId;
        }

        public AggregationSwitch GetModel()
        {
            return new AggregationSwitch
            {
                Id=this.Id, 
                IPAddress=this.IPAddress,
                IPMask = this.IPMask, 
                MACAddress=this.MACAddress,
                Description =this.Description,
                CreateDate = this.CreateDate,
                Status = this.Status,
                ManufacturerId = this.ManufacturerId,
                PostalAddressId = this.PostalAddressId,
                SwitchPortId = this.SwitchPortId,
                CoreSwitchId = this.CoreSwitchId,
            };
        }

    }

}
