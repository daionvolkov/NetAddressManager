using NetAddressManager.Api.Models.Enums;
using NetAddressManager.Client.Models;
using NetAddressManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetAddressManager.Client.Services.HandlerServices
{
    public class AggregationSwitchHandlerService
    {
        private AuthToken _token;
        private CoreSwitchRequestService _coreSwitchRequestService;
        private AggregationSwitchRequestService _aggregationSwitchRequestService;
        private PostalAddressHandlerService _postalAddressHandlerService;
        private SwitchPortHandlerService _switchPortHandlerService;
        private EquipmentHandlerService _equipmentHandlerService;

        public AggregationSwitchHandlerService(AuthToken token)
        {
            _token = token;
            _coreSwitchRequestService = new CoreSwitchRequestService();
            _postalAddressHandlerService = new PostalAddressHandlerService(_token);
            _switchPortHandlerService = new SwitchPortHandlerService(_token);
            _equipmentHandlerService = new EquipmentHandlerService(_token);
            _aggregationSwitchRequestService = new AggregationSwitchRequestService();
        }

        public SwitchDetailsModel GetAggregationSwitchClient(AggregationSwitchModel aggregationSwitch)
        {
            int switchId = aggregationSwitch.Id;

            string ipGatewayData = string.Empty;
            SwitchType switchType = SwitchType.Aggregation;
            int equipmentId = Convert.ToInt32(aggregationSwitch.EquipmentManufacturerId);
            int addressId = Convert.ToInt32(aggregationSwitch.PostalAddressId);

            var aggregationSwitchData = _aggregationSwitchRequestService.GetAggregationSwitchById(_token, switchId);
            List<int>? portIds = aggregationSwitchData.SwitchPortIds;
            List<SwitchPortModel> PortDetails = new List<SwitchPortModel>();
            string addressStr = _postalAddressHandlerService.GetPostalAddressClient(addressId);
            string equipmentManufacturerStr = _equipmentHandlerService.GetEquipmentClient(equipmentId);
            if (portIds != null)
            {
                PortDetails = _switchPortHandlerService.LoadPortDetails(portIds);
            }


            if (aggregationSwitch.CoreSwitchId != null)
                ipGatewayData = _coreSwitchRequestService.GetCoreSwitchById(_token, (int)aggregationSwitch.CoreSwitchId).IPAddress;

            SwitchDetailsModel switchDetailsModel = new SwitchDetailsModel
            {
                SwitchData = aggregationSwitchData,
                IPGateway = ipGatewayData,
                SwitchType = switchType,
                PostalAddress = addressStr,
                Equipment = equipmentManufacturerStr,
                Port = PortDetails,

            };
            return switchDetailsModel;
        }
    }
}
