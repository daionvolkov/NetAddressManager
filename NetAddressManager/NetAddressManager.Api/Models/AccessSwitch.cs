namespace NetAddressManager.Api.Models
{
    public class AccessSwitch : CommonObject
    {
        public int? AggregationSwitchId { get; set; }
        public AggregationSwitch? AggregationSwitch { get; set; }
    }
}
