using NetAddressManager.Api.Models.Abstractions;
using NetAddressManager.Models;

namespace NetAddressManager.Api.Models.Services
{
    public class AccessSwitchService : AbstractionService, ICommonService<AccessSwitchModel>
    {
        private readonly ApplicationContext _db;

        public AccessSwitchService(ApplicationContext db)
        {
            _db = db;
        }

        public AccessSwitchModel Get(int id)
        {
            AccessSwitch accessSwitch=_db.AccessSwitch.FirstOrDefault(asw=>asw.Id == id) ?? new AccessSwitch();
            return accessSwitch.GetModel();
        }

        public bool Create(AccessSwitchModel model)
        {
            bool result = DoAction(delegate ()
            {
                AccessSwitch accessSwitch = new AccessSwitch(model);
                _db.AccessSwitch.Add(accessSwitch);
                _db.SaveChanges();
            });
            return result;
        }

        public bool Delete(int id)
        {
            bool result = DoAction(delegate ()
            {
                AccessSwitch accessSwitch = _db.AccessSwitch
                .FirstOrDefault(asw => asw.Id == id) ?? new AccessSwitch();
                _db.AccessSwitch.Remove(accessSwitch);
                _db.SaveChanges();
            });
            return result;
        }



        public bool Update(int id, AccessSwitchModel model)
        {
            bool result = DoAction(delegate ()
            {
                AccessSwitch accessSwitch = _db.AccessSwitch.FirstOrDefault(asw => asw.Id == id) ?? new AccessSwitch();

                accessSwitch.IPAddress = model.IPAddress;
                accessSwitch.IPMask = model.IPMask;
                accessSwitch.MACAddress = model.MACAddress;
                accessSwitch.Description = model.Description;
                accessSwitch.Status = model.Status;

                _db.AccessSwitch.Update(accessSwitch);
                _db.SaveChanges();
            });
            return result;
        }

        public void AddPostalAddressToAccess(int id, int addressId)
        {
            AccessSwitch accessSwitch = _db.AccessSwitch.FirstOrDefault(asw => asw.Id == id) ?? new AccessSwitch();
            var address = _db.EquipmentManufacturer.FirstOrDefault(e => e.Id == addressId) ?? new EquipmentManufacturer();
            if (accessSwitch.PostalAddressId != address.Id)
            {
                accessSwitch.PostalAddressId = address.Id;
                _db.AccessSwitch.Update(accessSwitch);
            }
            _db.SaveChanges();
        }

        public void AddEquipmentToAccess(int id, int equipmentId)
        {
            AccessSwitch accessSwitch = _db.AccessSwitch.FirstOrDefault(asw => asw.Id == id) ?? new AccessSwitch();
            var equipment = _db.EquipmentManufacturer.FirstOrDefault(e => e.Id == equipmentId) ?? new EquipmentManufacturer();
            if (accessSwitch.EquipmentManufacturerId != equipment.Id)
            {
                accessSwitch.EquipmentManufacturerId = equipment.Id;
                _db.AccessSwitch.Update(accessSwitch);
            }
            _db.SaveChanges();
        }

        public void AddGatewayToAccess(int id, int gatewayId)
        {
            AccessSwitch accessSwitch = _db.AccessSwitch.FirstOrDefault(asw => asw.Id == id) ?? new AccessSwitch();
            var gateway = _db.AggregationSwitch.FirstOrDefault(g => g.Id == gatewayId) ?? new AggregationSwitch();
            if (accessSwitch.AggregationSwitchId != gateway.Id)
            {
                accessSwitch.AggregationSwitchId = gateway.Id;
                _db.AccessSwitch.Update(accessSwitch);
            }
            _db.SaveChanges();
        }
    }
}
