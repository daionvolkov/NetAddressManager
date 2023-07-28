using NetAddressManager.Api.Models.Enums;
using NetAddressManager.Client.Models;
using NetAddressManager.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NetAddressManager.Client.Services.HandlerServices
{
    public class SwitchPortHandlerService
    {
        private AuthToken _token;
        private SwitchPortRequestService _switchPortRequestService;
        private CoreSwitchRequestService _coreSwitchRequestService;
        private AggregationSwitchRequestService _aggregationSwitchRequestService;
        private AccessSwitchRequestService _accessSwitchRequestService;
        private CommonViewService _commonViewService; 
        public SwitchPortHandlerService(AuthToken token) 
        {
            _token = token;
            _switchPortRequestService = new SwitchPortRequestService();
            _coreSwitchRequestService = new CoreSwitchRequestService();
            _aggregationSwitchRequestService = new AggregationSwitchRequestService();
            _accessSwitchRequestService = new AccessSwitchRequestService();
            _commonViewService = new CommonViewService();
        }



        #region METHODS
        public void CreatePortClient(SwitchPortModel SelectedPort, SwitchDetailsModel SwitchDetailsModel)
        {
            var resultActionPortToSwitch = HttpStatusCode.BadRequest;
            List<int>? ports = new List<int>();
            int maxId = GetMaxIdFromPorts();

            ports.Add(maxId + 1);
            var resultActionPort = _switchPortRequestService.CreateSwitchPort(_token, SelectedPort);

            if (SwitchDetailsModel.SwitchType == SwitchType.Core && resultActionPort == HttpStatusCode.OK)
            {
                resultActionPortToSwitch = _coreSwitchRequestService.AddPortsToCoreSwitch(_token, SwitchDetailsModel.SwitchData.Id, ports);
            }
            if (SwitchDetailsModel.SwitchType == SwitchType.Aggregation && resultActionPort == HttpStatusCode.OK)
            {
                resultActionPortToSwitch = _aggregationSwitchRequestService.AddPortsToAggregationSwitch(_token, SwitchDetailsModel.SwitchData.Id, ports);
            }
            if (SwitchDetailsModel.SwitchType == SwitchType.Access && resultActionPort == HttpStatusCode.OK)
            {
                resultActionPortToSwitch = _accessSwitchRequestService.AddPortsToAccessSwitch(_token, SwitchDetailsModel.SwitchData.Id, ports);
            }
            _commonViewService.ShowActionRelust(resultActionPortToSwitch, "Порт создан");
        }

        public void UpdatePortClient(SwitchPortModel SelectedPort)
        {
            var resultAction = _switchPortRequestService.UpdateSwitchPort(_token, SelectedPort);
            _commonViewService.ShowActionRelust(resultAction, "Порт изменен");
        }


        private int GetMaxIdFromPorts()
        {
            var ports = _switchPortRequestService.GetAllSwitchPorts(_token);
            if (ports == null || ports.Count == 0)
            {
                return -1;
            }

            int maxId = ports.Max(port => port.Id);
            return maxId;
        }


        public List<SwitchPortModel> LoadPortDetails(List<int> portIds)
        {
            List<SwitchPortModel> PortDetails = new List<SwitchPortModel>();
            List<SwitchPortModel> switchPortModels = new List<SwitchPortModel>();
            foreach (int portId in portIds)
            {
                SwitchPortModel switchPortModel = _switchPortRequestService.GetSwitchPortById(_token, portId);

                if (switchPortModel != null)
                {
                    PortDetails.Add(switchPortModel);
                }
            }
            return PortDetails;
        }
        #endregion
    }
}
