﻿using NetAddressManager.Models;

namespace NetAddressManager.Api.Models
{
    public class PostalAddress
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }

        public PostalAddress()
        {
        }

        public PostalAddress(PostalAddressModel postalModel)
        {
            Id = postalModel.Id;
            City = postalModel.City;
            Street = postalModel.Street;
            Building = postalModel.Building;
        }
        
        public PostalAddress GetModel()
        {
            return new PostalAddress() { 
                Id = this.Id, 
                City = this.City, 
                Street = this.Street, 
                Building = this.Building };
        }
    }

  
}
