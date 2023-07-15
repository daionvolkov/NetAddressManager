using NetAddressManager.Api.Models.Abstractions;
using NetAddressManager.Api.Models.Enums;
using NetAddressManager.Models;

namespace NetAddressManager.Api.Models
{
    public class SwitchPort 
    {
        public int Id { get; set; }
        public int PortNumber { get; set; }
        public string? Description { get; set; }
        public PortStatus Status { get; set; }
        public SwitchType Type { get; set; }

        public SwitchPort() { }

        public SwitchPort(SwitchPortModel switchPortModel)
        {
            Id = switchPortModel.Id;
            PortNumber = switchPortModel.PortNumber;
            Description = switchPortModel.Description;
            Status = switchPortModel.Status;
            Type = switchPortModel.Type;
        }

        public SwitchPortModel GetModel()
        {
            return new SwitchPortModel
            {
                Id = this.Id, 
                PortNumber = this.PortNumber, 
                Description= this.Description, 
                Status = this.Status, 
                Type = this.Type 
            };
        }
    }
}
