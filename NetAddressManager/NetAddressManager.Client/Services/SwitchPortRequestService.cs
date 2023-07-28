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
    public class SwitchPortRequestService : CommonRequestService
    {
        private string _switchPortControllerUrl = HOST + "switchport";


        public List<SwitchPortModel> GetAllSwitchPorts(AuthToken token)
        {
            string response = GetDataByUrl(HttpMethod.Get, _switchPortControllerUrl, token);
            List<SwitchPortModel> switchPorts = JsonConvert.DeserializeObject<List<SwitchPortModel>>(response) ?? new List<SwitchPortModel>();
            return switchPorts;
        }

        public SwitchPortModel GetSwitchPortById(AuthToken token, int switchPortId)
        {
            var response = GetDataByUrl(HttpMethod.Get, _switchPortControllerUrl + $"/{switchPortId}", token);
            SwitchPortModel switchPort = JsonConvert.DeserializeObject<SwitchPortModel>(response) ?? new SwitchPortModel();
            return switchPort;
        }

        public HttpStatusCode CreateSwitchPort(AuthToken token, SwitchPortModel switchPort)
        {
            string switchPortJson = JsonConvert.SerializeObject(switchPort);
            var result = SendDataByUrl(HttpMethod.Post, _switchPortControllerUrl, token, switchPortJson);
            return result;
        }

        public HttpStatusCode UpdateSwitchPort(AuthToken token, SwitchPortModel switchPort)
        {
            string switchPortJson = JsonConvert.SerializeObject(switchPort);
            var result = SendDataByUrl(HttpMethod.Patch, _switchPortControllerUrl + $"/{switchPort.Id}", token, switchPortJson);
            return result;
        }
    }
}
