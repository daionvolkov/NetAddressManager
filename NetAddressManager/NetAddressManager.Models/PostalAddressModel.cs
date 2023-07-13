using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetAddressManager.Models
{
    public class PostalAddressModel
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }

        public PostalAddressModel()
        {
        }

        public PostalAddressModel(string city, string street, string building)
        {
            City = city;
            Street = street;
            Building = building;
        }

        public override string ToString()
        {
            return $"{City} {Street} {Building}";
        }
    }

   
}
