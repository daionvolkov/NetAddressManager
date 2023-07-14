using Microsoft.EntityFrameworkCore;

namespace NetAddressManager.Api.Models.Services
{
    public class AddressSearchService
    {
        private readonly ApplicationContext _db;


        public AddressSearchService(ApplicationContext db)
        {
            _db = db;
        }

        public async Task<SwitchData> GetAddressDataAsync(string address)
        {
            var postalAddress = await GetPostalAddressAsync(address);

            if (postalAddress == null)
            {
                return null;
            }

            var postalAddressId = postalAddress.Id;

            var coreSwitchData = await GetCoreSwitchDataAsync(postalAddressId);
            var aggregationSwitchData = await GetAggregationSwitchDataAsync(postalAddressId);
            var accessSwitchData = await GetAccessSwitchDataAsync(postalAddressId);

            var equipmentManufacturerIds = GetEquipmentManufacturerIds(coreSwitchData, aggregationSwitchData, accessSwitchData);
            var equipmentManufacturers = await GetEquipmentManufacturersAsync(equipmentManufacturerIds);

            return new SwitchData
            {
                CoreSwitchData = coreSwitchData,
                AggregationSwitchData = aggregationSwitchData,
                AccessSwitchData = accessSwitchData,
                EquipmentManufacturers = equipmentManufacturers
            };
        }

        private async Task<PostalAddress> GetPostalAddressAsync(string address)
        {
            return await _db.PostalAddress.FirstOrDefaultAsync(a => (a.City == address) || (a.Street == address) || (a.Building == address));
        }


        private async Task<List<CoreSwitch>> GetCoreSwitchDataAsync(int postalAddressId)
        {
            return await _db.CoreSwitch.Where(s => s.PostalAddressId == postalAddressId).ToListAsync();
        }


        private async Task<List<AggregationSwitch>> GetAggregationSwitchDataAsync(int postalAddressId)
        {
            return await _db.AggregationSwitch.Where(s => s.PostalAddressId == postalAddressId).ToListAsync();
        }



        private async Task<List<AccessSwitch>> GetAccessSwitchDataAsync(int postalAddressId)
        {
            return await _db.AccessSwitch.Where(s => s.PostalAddressId == postalAddressId).ToListAsync();
        }

        private List<int> GetEquipmentManufacturerIds(List<CoreSwitch> coreSwitchData, List<AggregationSwitch> aggregationSwitchData, List<AccessSwitch> accessSwitchData)
        {
            var equipmentManufacturerIds = coreSwitchData
                .Select(s => s.EquipmentManufacturerId)
                .Concat(aggregationSwitchData.Select(s => s.EquipmentManufacturerId))
                .Concat(accessSwitchData.Select(s => s.EquipmentManufacturerId))
                .Where(id => id.HasValue)
                .Select(id => id.Value)
                .Distinct()
                .ToList();

            return equipmentManufacturerIds;
        }
        private async Task<List<EquipmentManufacturer>> GetEquipmentManufacturersAsync(List<int> equipmentManufacturerIds)
        {
            return await _db.EquipmentManufacturer.Where(m => equipmentManufacturerIds.Contains(m.Id)).ToListAsync();
        }
    }
}
