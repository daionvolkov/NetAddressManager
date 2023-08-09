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
            return postalAddressModel ?? new PostalAddressModel();
        } 

        public EquipmentManufacturerModel IsEquipmentExists(AuthToken token, string equipment)
        {
            string response = GetDataByUrl(HttpMethod.Get, _checkDataControllerUrl + $"/{equipment}/equipment", token);
            EquipmentManufacturerModel? equipmentManufacturerModel = JsonConvert.DeserializeObject<EquipmentManufacturerModel>(response);
            return equipmentManufacturerModel ?? new EquipmentManufacturerModel();
        }

        public CoreSwitchModel IsCoreSwitchExistst(AuthToken token, string ipAddress)
        {
            string response = GetDataByUrl(HttpMethod.Get, _checkDataControllerUrl + $"/{ipAddress}/core", token);
            CoreSwitchModel? coreSwitchModel = JsonConvert.DeserializeObject<CoreSwitchModel>(response);
            return coreSwitchModel ?? new CoreSwitchModel();
        }

        public AggregationSwitchModel IsAggegationSwitchExists(AuthToken token, string ipAddress)
        {
            string response = GetDataByUrl(HttpMethod.Get, _checkDataControllerUrl + $"/{ipAddress}/aggregation", token);
            AggregationSwitchModel? aggregationSwitchModel = JsonConvert.DeserializeObject<AggregationSwitchModel>(response);
            return aggregationSwitchModel ?? new AggregationSwitchModel();
        }

        public AccessSwitchModel IsAccessSwitchExists(AuthToken token, string ipAddress)
        {
            string response = GetDataByUrl(HttpMethod.Get, _checkDataControllerUrl + $"/{ipAddress}/access", token);
            AccessSwitchModel? accessSwitchModel = JsonConvert.DeserializeObject<AccessSwitchModel>(response);
            return accessSwitchModel ?? new AccessSwitchModel();
        }
    }
}
