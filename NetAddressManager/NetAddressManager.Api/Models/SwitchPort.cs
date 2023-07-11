using NetAddressManager.Api.Models.Enums;

namespace NetAddressManager.Api.Models
{
    public class SwitchPort
    {
        public int Id { get; set; }
        public int PortNumber { get; set; }
        public string? Description { get; set; }
        public PortStatus Status { get; set; }
        public SwitchType Type { get; set; }
    }
}
