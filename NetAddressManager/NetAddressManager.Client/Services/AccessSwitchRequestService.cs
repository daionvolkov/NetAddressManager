using NetAddressManager.Client.Models;
using NetAddressManager.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NetAddressManager.Client.Services
{
    public class AccessSwitchRequestService : CommonRequestService
    {
        private string _accessSwitchesControllerUrl = HOST + "accessswitch";

        public List<AccessSwitchModel> GetAllAccessSwitches(AuthToken token)
        {
            string response = GetDataByUrl(HttpMethod.Get, _accessSwitchesControllerUrl, token);
            List<AccessSwitchModel> accessSwitches = JsonConvert.DeserializeObject<List<AccessSwitchModel>>(response) ?? new List<AccessSwitchModel>();
            return accessSwitches;
        }

        public AccessSwitchModel GetAccessSwitchById(AuthToken token, int accessSwitchModelId)
        {
            var response = GetDataByUrl(HttpMethod.Get, _accessSwitchesControllerUrl + $"/{accessSwitchModelId}", token);
            AccessSwitchModel accessSwitch = JsonConvert.DeserializeObject<AccessSwitchModel>(response) ?? new AccessSwitchModel();
            return accessSwitch;
        }

        public HttpStatusCode CreateAccessSwitch(AuthToken token, AccessSwitchModel accessSwitch)
        {
            string accessSwitchJson = JsonConvert.SerializeObject(accessSwitch);
            var result = SendDataByUrl(HttpMethod.Post, _accessSwitchesControllerUrl, token, accessSwitchJson);
            return result;
        }

        public HttpStatusCode UpdateAccessSwitch(AuthToken token, AccessSwitchModel accessSwitch)
        {
            string accessSwitchJson = JsonConvert.SerializeObject(accessSwitch);
            var result = SendDataByUrl(HttpMethod.Patch, _accessSwitchesControllerUrl + $"/{accessSwitch.Id}", token, accessSwitchJson);
            return result;
        }

        public HttpStatusCode DeleteAccessSwitch(AuthToken token, int accessSwitchId)
        {
            var result = DeleteDataByUrl(_accessSwitchesControllerUrl + $"/{accessSwitchId}", token);
            return result;
        }

        public HttpStatusCode AddPostalAddressToAccessSwitch(AuthToken token, int accessSwitchId, int postalAddressId)
        {
            string postalAddressIdJson = JsonConvert.SerializeObject(postalAddressId);
            var result = SendDataByUrl(HttpMethod.Patch, _accessSwitchesControllerUrl + $"/{accessSwitchId}/address", token, postalAddressIdJson);
            return result;
        }

        public HttpStatusCode AddEquipmentToAccessSwitch(AuthToken token, int accessSwitchId, int equipmentId)
        {
            string equipmentIdJson = JsonConvert.SerializeObject(equipmentId);
            var result = SendDataByUrl(HttpMethod.Patch, _accessSwitchesControllerUrl + $"/{accessSwitchId}/equipment", token, equipmentIdJson);
            return result;
        }

        public HttpStatusCode AddPortsToAccessSwitch(AuthToken token, int accessSwitchId, List<int> portIds)
        {
            string portIdsJson = JsonConvert.SerializeObject(portIds);
            var result = SendDataByUrl(HttpMethod.Patch, _accessSwitchesControllerUrl + $"/{accessSwitchId}/port", token, portIdsJson);
            return result;
        }

    }
}
