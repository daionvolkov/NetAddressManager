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
    public  class AccessSwitchHandlerService
    {
        private AuthToken _token;
        private AggregationSwitchRequestService _aggregationSwitchRequestService;
        private AccessSwitchRequestService _accessSwitchRequestService;
        private PostalAddressHandlerService _postalAddressHandlerService;
        private SwitchPortHandlerService _switchPortHandlerService;
        private EquipmentHandlerService _equipmentHandlerService;

        public AccessSwitchHandlerService(AuthToken token)
        {
            _token = token;
            _postalAddressHandlerService = new PostalAddressHandlerService(_token);
            _switchPortHandlerService = new SwitchPortHandlerService(_token);
            _equipmentHandlerService = new EquipmentHandlerService(_token);
            _aggregationSwitchRequestService = new AggregationSwitchRequestService();
            _accessSwitchRequestService = new AccessSwitchRequestService();
        }

        public SwitchDetailsModel GetAccessSwitchClient(AccessSwitchModel accessSwitch)
        {
            int switchId = accessSwitch.Id;

            string ipGatewayData = string.Empty;
            int addressId = Convert.ToInt32(accessSwitch.PostalAddressId);
            int equipmentId = Convert.ToInt32(accessSwitch.EquipmentManufacturerId);
            SwitchType switchType = SwitchType.Access;

            var accessSwitchData = _accessSwitchRequestService.GetAccessSwitchById(_token, switchId);
            string equipmentManufacturerStr = _equipmentHandlerService.GetEquipmentClient(equipmentId);
            string addressStr = _postalAddressHandlerService.GetPostalAddressClient(addressId);
            List<int>? portIds = accessSwitchData.SwitchPortIds;
            List<SwitchPortModel> PortDetails = new List<SwitchPortModel>();
            if (portIds != null)
            {
                PortDetails = _switchPortHandlerService.LoadPortDetails(portIds);
            }
            if (accessSwitch.AggregationSwitchId != null)
                ipGatewayData = _aggregationSwitchRequestService.GetAggregationSwitchById(_token, (int)accessSwitch.AggregationSwitchId).IPAddress;

            SwitchDetailsModel switchDetailsModel = new SwitchDetailsModel
            {
                SwitchData = accessSwitchData,
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
