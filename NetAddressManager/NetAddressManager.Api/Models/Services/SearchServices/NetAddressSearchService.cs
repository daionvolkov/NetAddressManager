using Microsoft.EntityFrameworkCore;
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

        public async Task<SwitchData> SearchSwitchByIpAddressAsync(string ipAddress)
        {
            var coreSwitch = await SearchCoreSwitchByIpAddressAsync(ipAddress);
            if (coreSwitch != null)
            {
                var equipmentManufacturer = await GetEquipmentManufacturerByIdAsync((int)coreSwitch.EquipmentManufacturerId);
                var postalAddress = await GetPostalAddressByIdAsync((int)coreSwitch.PostalAddressId);
                return new SwitchData
                {
                    CoreSwitchData = new List<CoreSwitch> { coreSwitch },
                    EquipmentManufacturers = new List<EquipmentManufacturer> { equipmentManufacturer },
                    PostalAddresses = new List<PostalAddress> { postalAddress } 
                };
            }
            var aggregationSwitch = await SearchAggregationSwitchByIpAddressAsync(ipAddress);
            if (aggregationSwitch != null)
            {
                var equipmentManufacturer = await GetEquipmentManufacturerByIdAsync((int)aggregationSwitch.EquipmentManufacturerId);
                var postalAddress = await GetPostalAddressByIdAsync((int)aggregationSwitch.PostalAddressId);
                return new SwitchData
                {
                    AggregationSwitchData = new List<AggregationSwitch> { aggregationSwitch },
                    EquipmentManufacturers = new List<EquipmentManufacturer> { equipmentManufacturer },
                    PostalAddresses = new List<PostalAddress> { postalAddress }
                };
            }

            var accessSwitch = await SearchAccessSwitchByIpAddressAsync(ipAddress);
            if (accessSwitch != null)
            {
                var equipmentManufacturer = await GetEquipmentManufacturerByIdAsync((int)accessSwitch.EquipmentManufacturerId);
                var postalAddress = await GetPostalAddressByIdAsync((int)accessSwitch.PostalAddressId);

                return new SwitchData
                {
                    AccessSwitchData = new List<AccessSwitch> { accessSwitch },
                    EquipmentManufacturers = new List<EquipmentManufacturer> { equipmentManufacturer },
                    PostalAddresses = new List<PostalAddress> { postalAddress }
                };
            }
            return null; 
        }

        private async Task<CoreSwitch> SearchCoreSwitchByIpAddressAsync(string ipAddress)
        {
            return await _db.CoreSwitch.FirstOrDefaultAsync(s => s.IPAddress == ipAddress);
        }

        private async Task<AggregationSwitch> SearchAggregationSwitchByIpAddressAsync(string ipAddress)
        {
            return await _db.AggregationSwitch.FirstOrDefaultAsync(s => s.IPAddress == ipAddress);
        }

        private async Task<AccessSwitch> SearchAccessSwitchByIpAddressAsync(string ipAddress)
        {
            return await _db.AccessSwitch.FirstOrDefaultAsync(s => s.IPAddress == ipAddress);
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
