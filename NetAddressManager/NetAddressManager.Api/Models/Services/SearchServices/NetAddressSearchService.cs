using Microsoft.EntityFrameworkCore;
using NetAddressManager.Models;
using System.Security.AccessControl;

namespace NetAddressManager.Api.Models.Services.SearchServices
{
    public class NetAddressSearchService
    {
        private readonly ApplicationContext _db;

        public NetAddressSearchService(ApplicationContext db)
        {
            _db = db;
        }

        public async Task<SwitchDataModel> SearchSwitchByIpAddressAsync(string ipAddress)
        {

            var switchDataModel = new SwitchDataModel();

            
            var coreSwitch = await _db.CoreSwitch.FirstOrDefaultAsync(s => s.IPAddress == ipAddress);
            if (coreSwitch != null)
            {
                switchDataModel.CoreSwitchData.Add(coreSwitch.GetModel());
                if(coreSwitch.EquipmentManufacturerId !=null)
                {
                    EquipmentManufacturer equipment = await GetEquipmentManufacturerByIdAsync((int)coreSwitch.EquipmentManufacturerId);
                    switchDataModel.EquipmentManufacturers.Add(equipment.GetModel());

                }
                if (coreSwitch.PostalAddressId != null)
                {
                    PostalAddress postalAddress = await GetPostalAddressByIdAsync((int)coreSwitch.PostalAddressId);
                    switchDataModel.PostalAddresses.Add(postalAddress.GetModel());
                }
          
                return switchDataModel;
            }

            
            var aggregationSwitch = await _db.AggregationSwitch.FirstOrDefaultAsync(s => s.IPAddress == ipAddress);
            if (aggregationSwitch != null)
            {
                switchDataModel.AggregationSwitchData.Add(aggregationSwitch.GetModel());
                if (aggregationSwitch.EquipmentManufacturerId != null)
                {
                    EquipmentManufacturer equipment = await GetEquipmentManufacturerByIdAsync((int)aggregationSwitch.EquipmentManufacturerId);
                    switchDataModel.EquipmentManufacturers.Add(equipment.GetModel());

                }
                if (aggregationSwitch.PostalAddressId != null)
                {
                    PostalAddress postalAddress = await GetPostalAddressByIdAsync((int)aggregationSwitch.PostalAddressId);
                    switchDataModel.PostalAddresses.Add(postalAddress.GetModel());
                }

                return switchDataModel;
            }

            
            var accessSwitch = await _db.AccessSwitch.FirstOrDefaultAsync(s => s.IPAddress == ipAddress);
            if (accessSwitch != null)
            {
                switchDataModel.AccessSwitchData.Add(accessSwitch.GetModel());
                if (accessSwitch.EquipmentManufacturerId != null)
                {
                    EquipmentManufacturer equipment = await GetEquipmentManufacturerByIdAsync((int)accessSwitch.EquipmentManufacturerId);
                    switchDataModel.EquipmentManufacturers.Add(equipment.GetModel());

                }
                if (accessSwitch.PostalAddressId != null)
                {
                    PostalAddress postalAddress = await GetPostalAddressByIdAsync((int)accessSwitch.PostalAddressId);
                    switchDataModel.PostalAddresses.Add(postalAddress.GetModel());
                }

                return switchDataModel;
            }

            return null;
        }
        private async Task<EquipmentManufacturer> GetEquipmentManufacturerByIdAsync(int equipmentManufacturerId)
        {
            return await _db.EquipmentManufacturer.FirstOrDefaultAsync(m => m.Id == equipmentManufacturerId);
        }

        private async Task<PostalAddress> GetPostalAddressByIdAsync(int postalAddressId)
        {
            return await _db.PostalAddress.FirstOrDefaultAsync(m => m.Id == postalAddressId);
        }
    }
}
