using NetAddressManager.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NetAddressManager.Client.Models;

namespace NetAddressManager.Client.Services
{
    public class CheckDataRequestService : CommonRequestService
    {
        private readonly string _checkDataControllerUrl = HOST + "checkdata";
        public bool IsSwitchExists(AuthToken token, string ipAddress)
        {
            string response = GetDataByUrl(HttpMethod.Get, _checkDataControllerUrl + $"/{ipAddress}/netAddress", token);
            bool isExists = JsonConvert.DeserializeObject<bool>(response);
            return isExists;
        }

        public PostalAddressModel IsPostalAddressExists(AuthToken token, string address)
        {
            string response = GetDataByUrl(HttpMethod.Get, _checkDataControllerUrl + $"/{address}/address", token);
            PostalAddressModel? postalAddressModel = JsonConvert.DeserializeObject<PostalAddressModel>(response);
            return postalAddressModel;
        }

        public bool IsEquipmentExists(AuthToken token, string equipment)
        {
            string response = GetDataByUrl(HttpMethod.Get, _checkDataControllerUrl + $"/{equipment}/equipment", token);
            bool isExists = JsonConvert.DeserializeObject<bool>(response);
            return isExists;
        }
    }
}
