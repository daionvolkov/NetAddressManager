using NetAddressManager.Client.Models;
using NetAddressManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetAddressManager.Client.Services.HandlerServices
{
    public class EquipmentHandlerService
    {
        private AuthToken _token;
        private EquipmentRequestService _equipmentRequestService;


        public EquipmentHandlerService(AuthToken token)
        {
            _token = token;
            _equipmentRequestService = new EquipmentRequestService();
        }
        public string GetEquipmentClient(int equipmentId)
        {
            string equipmentManufacturerStr = string.Empty;
            if (equipmentId != 0)
            {
                EquipmentManufacturerModel equipmentManufacturer = _equipmentRequestService.GetEquipmentById(_token, equipmentId);
                equipmentManufacturerStr = $"{equipmentManufacturer.Manufacturer}, {equipmentManufacturer.Model}";
            }
            return equipmentManufacturerStr;
        }
    }
}
