using NetAddressManager.Client.Models;
using NetAddressManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetAddressManager.Client.Services.HandlerServices
{
    public class PostalAddressHandlerService
    {
        private AuthToken _token;
        private PostalAddressRequestService _postalAddressRequestService;

        public PostalAddressHandlerService(AuthToken token)
        {
            _token = token;
            _postalAddressRequestService = new PostalAddressRequestService();
        }

        public string GetPostalAddressClient(int addressId)
        {
            string addressStr = string.Empty;
            if (addressId != 0)
            {
                PostalAddressModel address = _postalAddressRequestService.GetPostalAddressById(_token, addressId);
                addressStr = $"{address.City}, {address.Street}, {address.Building}";
            }
            return addressStr;
        }
    }
}
