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
    public class CoreSwitchRequestService : CommonRequestService
    {
        private string _coreSwitchesControllerUrl = HOST + "coreswitch";

        public List<CoreSwitchModel> GetAllCoreSwitches(AuthToken token)
        {
            string response = GetDataByUrl(HttpMethod.Get, _coreSwitchesControllerUrl, token);
            List<CoreSwitchModel> coreSwitches = JsonConvert.DeserializeObject<List<CoreSwitchModel>>(response) ?? new List<CoreSwitchModel>();
            return coreSwitches;
        }

        public CoreSwitchModel GetCoreSwitchById(AuthToken token, int coreSwitchId)
        {
            var response = GetDataByUrl(HttpMethod.Get, _coreSwitchesControllerUrl + $"/{coreSwitchId}", token);
            CoreSwitchModel coreSwitches = JsonConvert.DeserializeObject<CoreSwitchModel>(response) ?? new CoreSwitchModel();
            return coreSwitches;
        }

        public HttpStatusCode CreateCoreSwitch(AuthToken token, CoreSwitchModel coreSwitch)
        {
            string coreSwitchJson = JsonConvert.SerializeObject(coreSwitch);
            var result = SendDataByUrl(HttpMethod.Post, _coreSwitchesControllerUrl, token, coreSwitchJson);
            return result;
        }

        public HttpStatusCode UpdateCoreSwitch(AuthToken token, CoreSwitchModel coreSwitch)
        {
            string coreSwitchJson = JsonConvert.SerializeObject(coreSwitch);
            var result = SendDataByUrl(HttpMethod.Patch, _coreSwitchesControllerUrl + $"/{coreSwitch.Id}", token, coreSwitchJson);
            return result;
        }

        public HttpStatusCode DeleteCoreSwitch(AuthToken token, int coreSwitchId)
        {
            var result = DeleteDataByUrl(_coreSwitchesControllerUrl + $"/{coreSwitchId}", token);
            return result;
        }


        public HttpStatusCode AddPostalAddressToCoreSwitch(AuthToken token, int coreSwitchId ,int postalAddressId)
        {
            string postalAddressIdJson = JsonConvert.SerializeObject(postalAddressId);
            var result = SendDataByUrl(HttpMethod.Patch, _coreSwitchesControllerUrl + $"/{coreSwitchId}/address", token, postalAddressIdJson);
            return result;
        }

        public HttpStatusCode AddEquipmentToCoreSwitch(AuthToken token, int coreSwitchId, int equipmentId)
        {
            string equipmentIdJson = JsonConvert.SerializeObject(equipmentId);
            var result = SendDataByUrl(HttpMethod.Patch, _coreSwitchesControllerUrl + $"/{coreSwitchId}/equipment", token, equipmentIdJson);
            return result;
        }

        public HttpStatusCode AddPortsToCoreSwitch(AuthToken token, int coreSwitchId, List<int> portIds)
        {
            string portIdsJson = JsonConvert.SerializeObject(portIds);
            var result = SendDataByUrl(HttpMethod.Patch, _coreSwitchesControllerUrl + $"/{coreSwitchId}/port", token, portIdsJson);
            return result;
        }

        public HttpStatusCode AddAggregationSwitchToCoreSwitch(AuthToken token, int coreSwitchId, List<int> aggregationSwitchIds)
        {
            string aggregationSwitchIdsJson = JsonConvert.SerializeObject(aggregationSwitchIds);
            var result = SendDataByUrl(HttpMethod.Patch, _coreSwitchesControllerUrl + $"/{coreSwitchId}/aggrswitch", token, aggregationSwitchIdsJson);
            return result;
        }

        public HttpStatusCode RemoveAggregationSwitchFromCoreSwitch(AuthToken token, int coreSwitchId, List<int> aggregationSwitchIds)
        {
            string aggregationSwitchIdsJson = JsonConvert.SerializeObject(aggregationSwitchIds);
            var result = SendDataByUrl(HttpMethod.Patch, _coreSwitchesControllerUrl + $"/{coreSwitchId}/aggrswitch/remove", token, aggregationSwitchIdsJson);
            return result;
        }
    }
}
