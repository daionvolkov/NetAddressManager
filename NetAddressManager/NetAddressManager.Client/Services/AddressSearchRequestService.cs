using NetAddressManager.Client.Models;
using NetAddressManager.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NetAddressManager.Client.Services
{
    public class AddressSearchRequestService : CommonRequestService
    {
        private string _addressSearchControllerUrl = HOST + "addresssearch";

        public Task<SwitchDataModel> GetSwitchesByAddress(AuthToken token, string address)
        {
            string response = GetDataByUrl(HttpMethod.Get, _addressSearchControllerUrl + $"/{address}", token);
            SwitchDataModel switchData = JsonConvert.DeserializeObject<SwitchDataModel> (response);
            return Task.FromResult(switchData);
        }
    }
}
