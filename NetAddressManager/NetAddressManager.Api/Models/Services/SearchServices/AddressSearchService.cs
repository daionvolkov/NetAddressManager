using Microsoft.EntityFrameworkCore;
using NetAddressManager.Models;

namespace NetAddressManager.Api.Models.Services.SearchServices
{
    public class AddressSearchService
    {
        private readonly ApplicationContext _db;
        public AddressSearchService(ApplicationContext db)
        {
            _db = db;
        }

        public async Task<SwitchDataModel> GetAddressDataAsync(string address)
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

           


            return new SwitchDataModel
            {
                CoreSwitchData = coreSwitchData,
                AggregationSwitchData = aggregationSwitchData,
                AccessSwitchData = accessSwitchData,
                EquipmentManufacturers = equipmentManufacturers
            };
        }

        private async Task<PostalAddress> GetPostalAddressAsync(string address)
        {
            var postalAddress = await _db.PostalAddress.FirstOrDefaultAsync(a => a.City == address || a.Street == address || a.Building == address );
            return postalAddress;
        }


        private async Task<List<CoreSwitchModel>> GetCoreSwitchDataAsync(int postalAddressId)
        {
            return await _db.CoreSwitch.Where(s => s.PostalAddressId == postalAddressId).Select(s => s.GetModel()).ToListAsync();
        }


        private async Task<List<AggregationSwitchModel>> GetAggregationSwitchDataAsync(int postalAddressId)
        {
            var aggrSwitch = await _db.AggregationSwitch.Where(s => s.PostalAddressId == postalAddressId).Select(s=>s.GetModel()).ToListAsync();
            return aggrSwitch;
        }


        private async Task<List<AccessSwitchModel>> GetAccessSwitchDataAsync(int postalAddressId)
        {
            return await _db.AccessSwitch.Where(s => s.PostalAddressId == postalAddressId).Select(s => s.GetModel()).ToListAsync();
        }

        private List<int> GetEquipmentManufacturerIds(List<CoreSwitchModel> coreSwitchData, List<AggregationSwitchModel> aggregationSwitchData, List<AccessSwitchModel> accessSwitchData)
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


        private async Task<List<EquipmentManufacturerModel>> GetEquipmentManufacturersAsync(List<int> equipmentManufacturerIds)
        {
            return await _db.EquipmentManufacturer.Where(m => equipmentManufacturerIds.Contains(m.Id)).Select(s => s.GetModel()).ToListAsync();
        }
    }
}
