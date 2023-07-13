using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace NetAddressManager.Api.Models.Services
{
    public class AddressSearchService
    {
        private readonly ApplicationContext _db;
        private readonly PostalAddressService _postalAddressService;
        private readonly EquipmentManufacturerService _equipmentManufacturerService;
        private readonly CoreSwitchService _coreSwitchService;

        public AddressSearchService(ApplicationContext db)
        {
            _db = db;
        }
        public async Task<SwitchData> GetAddressDataAsync(string address)
        {
            var postalAddress = await _db.PostalAddress.FirstOrDefaultAsync(a => (a.City == address) || (a.Street == address) || (a.Building == address));

            if (postalAddress == null)
            {
                return null;
            }

            var postalAddressId = postalAddress.Id;

            var coreSwitchData = await _db.CoreSwitch
                .Where(s => s.PostalAddressId == postalAddressId)
                .ToListAsync();

            //var aggregationSwitchData = await _db.AggregationSwitch
            //    .Where(s => s.PostalAddressId == postalAddressId)
            //    .ToListAsync();

            //var accessSwitchData = await _db.AccessSwitch
            //    .Where(s => s.PostalAddressId == postalAddressId)
            //    .ToListAsync();

            var equipmentManufacturerIds = coreSwitchData
                .Select(s => s.EquipmentManufacturerId)
            //    .Concat(aggregationSwitchData.Select(s => s.EquipmentManufacturerId))
              //  .Concat(accessSwitchData.Select(s => s.EquipmentManufacturerId))
                .Distinct()
                .ToList();

            var equipmentManufacturers = await _db.EquipmentManufacturer
                .Where(m => equipmentManufacturerIds.Contains(m.Id))
                .ToListAsync();

            return new SwitchData
            {
                CoreSwitchData = coreSwitchData,
                //AggregationSwitchData = aggregationSwitchData,
                //AccessSwitchData = accessSwitchData,
                EquipmentManufacturers = equipmentManufacturers
            };
        }
    }
}
