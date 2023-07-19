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
    public class AggregationSwitchRequestService : CommonRequestService
    {
        private string _aggregationSwitchesControllerUrl = HOST + "aggregationswitch";

        public List<AggregationSwitchModel> GetAllAggregationSwitches(AuthToken token)
        {
            string response = GetDataByUrl(HttpMethod.Get, _aggregationSwitchesControllerUrl, token);
            List<AggregationSwitchModel> aggregationSwitches = JsonConvert.DeserializeObject<List<AggregationSwitchModel>>(response) ?? new List<AggregationSwitchModel>();
            return aggregationSwitches;
        }

        public AggregationSwitchModel GetAggregationSwitchById(AuthToken token, int aggregationSwitchId)
        {
            var response = GetDataByUrl(HttpMethod.Get, _aggregationSwitchesControllerUrl + $"/{aggregationSwitchId}", token);
            AggregationSwitchModel aggregationSwitches = JsonConvert.DeserializeObject<AggregationSwitchModel>(response) ?? new AggregationSwitchModel();
            return aggregationSwitches;
        }

        public HttpStatusCode CreateAggregationSwitch(AuthToken token, AggregationSwitchModel aggregationSwitch)
        {
            string aggregationSwitchJson = JsonConvert.SerializeObject(aggregationSwitch);
            var result = SendDataByUrl(HttpMethod.Post, _aggregationSwitchesControllerUrl, token, aggregationSwitchJson);
            return result;
        }

        public HttpStatusCode UpdateAggregationSwitch(AuthToken token, AggregationSwitchModel aggregationSwitch)
        {
            string aggregationSwitchJson = JsonConvert.SerializeObject(aggregationSwitch);
            var result = SendDataByUrl(HttpMethod.Patch, _aggregationSwitchesControllerUrl + $"/{aggregationSwitch.Id}", token, aggregationSwitchJson);
            return result;
        }

        public HttpStatusCode DeleteAggregationSwitch(AuthToken token, int aggregationSwitchId)
        {
            var result = DeleteDataByUrl(_aggregationSwitchesControllerUrl + $"/{aggregationSwitchId}", token);
            return result;
        }


        public HttpStatusCode AddPostalAddressToAggregationSwitch(AuthToken token, int aggregationSwitchId, int postalAddressId)
        {
            string postalAddressIdJson = JsonConvert.SerializeObject(postalAddressId);
            var result = SendDataByUrl(HttpMethod.Patch, _aggregationSwitchesControllerUrl + $"/{aggregationSwitchId}/address", token, postalAddressIdJson);
            return result;
        }

        public HttpStatusCode AddEquipmentToAggregationSwitch(AuthToken token, int aggregationSwitchId, int equipmentId)
        {
            string equipmentIdJson = JsonConvert.SerializeObject(equipmentId);
            var result = SendDataByUrl(HttpMethod.Patch, _aggregationSwitchesControllerUrl + $"/{aggregationSwitchId}/equipment", token, equipmentIdJson);
            return result;
        }

        public HttpStatusCode AddPortsToAggregationSwitch(AuthToken token, int aggregationSwitchId, List<int> portIds)
        {
            string portIdsJson = JsonConvert.SerializeObject(portIds);
            var result = SendDataByUrl(HttpMethod.Patch, _aggregationSwitchesControllerUrl + $"/{aggregationSwitchId}/port", token, portIdsJson);
            return result;
        }


        public HttpStatusCode AddAccessSwitchToAggregationSwitch(AuthToken token, int aggregationSwitchId, List<int> accessSwitchIds)
        {
            string accessSwitchIdsJson = JsonConvert.SerializeObject(accessSwitchIds);
            var result = SendDataByUrl(HttpMethod.Patch, _aggregationSwitchesControllerUrl + $"/{aggregationSwitchId}/accessswitch", token, accessSwitchIdsJson);
            return result;
        }

        public HttpStatusCode RemoveAccessSwitchFromAggregationSwitch(AuthToken token, int aggregationSwitchId, List<int> accessSwitchIds)
        {
            string accessSwitchIdsJson = JsonConvert.SerializeObject(accessSwitchIds);
            var result = SendDataByUrl(HttpMethod.Patch, _aggregationSwitchesControllerUrl + $"/{aggregationSwitchId}/accessswitch/remove", token, accessSwitchIdsJson);
            return result;
        }

    }
}
