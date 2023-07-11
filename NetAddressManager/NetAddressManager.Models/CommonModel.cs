using NetAddressManager.Api.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetAddressManager.Models
{
    public class CommonModel
    {
        public int Id { get; set; }
        public string IPAddress { get; set; }
        public string IPMask { get; set; }
        public string? MACAddress { get; set; }
        public string? Description { get; set; }
        public DateTime CreateDate { get; set; }
        public SwitchStatus Status { get; set; }
        public int? ManufacturerId { get; set; }

        public int? PostalAddressId;
        public int? SwitchPortId { get; set; }
        public List<int>? SwitchPortIds { get; set; } = new List<int>();

    }
}
