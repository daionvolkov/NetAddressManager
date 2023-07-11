namespace NetAddressManager.Api.Models
{
    public class AggregationSwitch : CommonObject
    {
        public int? CoreSwitchId { get; set; }
        public CoreSwitch? CoreSwitch { get; set; }
        public List<AccessSwitch> AccessSwitches { get; set; } = new List<AccessSwitch>();


    }

}
