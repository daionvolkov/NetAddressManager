namespace NetAddressManager.Api.Models
{
    public class CoreSwitch : CommonObject
    {
        public string IPGateway { get; set; }
        public List<AggregationSwitch> AggregationSwitches { get; set; } = new List<AggregationSwitch>();

    }

}
