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
    public class EquipmentSearchRequestService : CommonRequestService
    {
        private string _equipmentSearchControllerUrl = HOST + "equipmentsearch";

        public Task<SwitchDataModel> GetSwitchesByEquipment(AuthToken token, string equipment)
        {
            string response = GetDataByUrl(HttpMethod.Get, _equipmentSearchControllerUrl + $"/{equipment}", token);
            Task<SwitchDataModel> switchData = JsonConvert.DeserializeObject<Task<SwitchDataModel>>(response);
            return switchData;
        }
    }
}
