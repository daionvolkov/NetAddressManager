using NetAddressManager.Client.Models;
using NetAddressManager.Client.Services;
using NetAddressManager.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NetAddressManager.ClientTests.Services
{
    public class NetAddressSearchRequestService : CommonRequestService
    {
        private string _netAddressSearchControllerUrl = HOST + "netaddresssearch";

        public  Task<SwitchDataModel> GetSwitchesByNetAddress(AuthToken token, string netAddress)
        {
            string response = GetDataByUrl(HttpMethod.Get, _netAddressSearchControllerUrl + $"/{netAddress}", token);
            SwitchDataModel switchData = JsonConvert.DeserializeObject<SwitchDataModel>(response);
            return Task.FromResult(switchData);
        }
    }
}
