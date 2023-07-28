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
    public class CoreSwitchHandlerService
    {
        private AuthToken _token;
        private CoreSwitchRequestService _coreSwitchRequestService;
        private PostalAddressHandlerService _postalAddressHandlerService;
        private SwitchPortHandlerService _switchPortHandlerService;
        private EquipmentHandlerService _equipmentHandlerService;

        public CoreSwitchHandlerService(AuthToken token)
        {
            _token = token;
            _coreSwitchRequestService = new CoreSwitchRequestService();
            _postalAddressHandlerService = new PostalAddressHandlerService(_token);
            _switchPortHandlerService = new SwitchPortHandlerService(_token);
            _equipmentHandlerService = new EquipmentHandlerService(_token);
        }
        public SwitchDetailsModel GetCoreSwitchClient(CoreSwitchModel coreSwitch)
        {
            int switchId = coreSwitch.Id;
            var coreSwitchData = _coreSwitchRequestService.GetCoreSwitchById(_token, switchId);

            SwitchType switchType = SwitchType.Core;
            string ipGatewayData = coreSwitch.IPGateway;

            int equipmentId = Convert.ToInt32(coreSwitch.EquipmentManufacturerId);
            int addressId = Convert.ToInt32(coreSwitch.PostalAddressId);

            List<int>? portIds = coreSwitchData.SwitchPortIds;
            List<SwitchPortModel> PortDetails = new List<SwitchPortModel>();

            string equipmentManufacturerStr = _equipmentHandlerService.GetEquipmentClient(equipmentId);
            string addressStr = _postalAddressHandlerService.GetPostalAddressClient(addressId);
            if (portIds != null)
            {
                PortDetails = _switchPortHandlerService.LoadPortDetails(portIds);
            }

            SwitchDetailsModel switchDetailsModel = new SwitchDetailsModel
            {
                SwitchData = coreSwitchData,
                SwitchType = switchType,
                IPGateway = ipGatewayData,
                PostalAddress = addressStr,
                Equipment = equipmentManufacturerStr,
                Port = PortDetails,
            };

            return switchDetailsModel;
        }
    }
}
