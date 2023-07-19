using Microsoft.EntityFrameworkCore;
using NetAddressManager.Models;

namespace NetAddressManager.Api.Models.Services.SearchServices
{
    public class EquipmentSearchService
    {
        private readonly ApplicationContext _db;
        public EquipmentSearchService(ApplicationContext db)
        {
            _db = db;
        }


        public async Task<SwitchDataModel> GetEquipmentDataAsync(string equipment)
        {
            var manufacturereEquipment = await GetEquipmentAsync(equipment);
            if (manufacturereEquipment == null)
            {
                return null;
            }

            var equipmentId = manufacturereEquipment.Id;

            var coreSwitchData = await GetCoreSwitchDataAsync(equipmentId);
            var aggregationSwitchData = await GetAggregationSwitchDataAsync(equipmentId);
            var accessSwitchData = await GetAccessSwitchDataAsync(equipmentId);
            var postalAddressIds = GetPostalAddressIds(coreSwitchData, aggregationSwitchData, accessSwitchData);
            var postalAddresses = await GetPostalAddressAsync(postalAddressIds);

            return new SwitchDataModel
            {
                CoreSwitchData = coreSwitchData,
                AggregationSwitchData = aggregationSwitchData,
                AccessSwitchData = accessSwitchData,
                PostalAddresses = postalAddresses
            };
        }


        private async Task<EquipmentManufacturer> GetEquipmentAsync(string equipment)
        {
            return await _db.EquipmentManufacturer.FirstOrDefaultAsync(a => a.Manufacturer.Contains(equipment) || a.Model.Contains(equipment));
        }

        private async Task<List<CoreSwitchModel>> GetCoreSwitchDataAsync(int equipmentId)
        {
            return await _db.CoreSwitch.Where(s => s.EquipmentManufacturerId == equipmentId).Select(s=>s.GetModel()).ToListAsync();
        }


        private async Task<List<AggregationSwitchModel>> GetAggregationSwitchDataAsync(int equipmentId)
        {
            return await _db.AggregationSwitch.Where(s => s.EquipmentManufacturerId == equipmentId).Select(s => s.GetModel()).ToListAsync();
        }


        private async Task<List<AccessSwitchModel>> GetAccessSwitchDataAsync(int equipmentId)
        {
            return await _db.AccessSwitch.Where(s => s.EquipmentManufacturerId == equipmentId).Select(s => s.GetModel()).ToListAsync();
        }

        private List<int> GetPostalAddressIds(List<CoreSwitchModel> coreSwitchData, List<AggregationSwitchModel> aggregationSwitchData, List<AccessSwitchModel> accessSwitchData)
        {
            var postalAddressIds = coreSwitchData
                .Select(s => s.PostalAddressId)
                .Concat(aggregationSwitchData.Select(s => s.PostalAddressId))
                .Concat(accessSwitchData.Select(s => s.PostalAddressId))
                .Where(id => id.HasValue)
                .Select(id => id.Value)
                .Distinct()
                .ToList();

            return postalAddressIds;
        }

        private async Task<List<PostalAddressModel>> GetPostalAddressAsync(List<int> postalAddressIds)
        {
            return await _db.PostalAddress.Where(m => postalAddressIds.Contains(m.Id)).Select(s => s.GetModel()).ToListAsync();
        }
    }
}
