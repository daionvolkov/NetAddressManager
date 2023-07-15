using NetAddressManager.Api.Models.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetAddressManager.Models
{
    public class SwitchPortModel
    {
        public int Id { get; set; }
        public int PortNumber { get; set; }
        public string? Description { get; set; }
        public PortStatus Status { get; set; }
        public SwitchType Type { get; set; }

        public SwitchPortModel() { }

        public SwitchPortModel(int portNumber, string? description, PortStatus status=PortStatus.Free, SwitchType type=SwitchType.Indeterminate)
        {
            PortNumber = portNumber;
            Description = description;
            Status = status;
            Type = type;
        }

        public override string ToString()
        {
            return $"{PortNumber} {Description}" ;
        }
    }
}
