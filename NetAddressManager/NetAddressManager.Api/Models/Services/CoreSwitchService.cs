using NetAddressManager.Api.Models.Abstractions;
using NetAddressManager.Models;
using System.Net;

namespace NetAddressManager.Api.Models.Services
{
    public class CoreSwitchService : AbstractionService, ICommonService<CoreSwitchModel>
    {
        private readonly ApplicationContext _db;
        
        public CoreSwitchService(ApplicationContext db)
        {
            _db = db;
        }

        public CoreSwitchModel Get(int id)
        {
            CoreSwitch coreSwitch = _db.CoreSwitch.FirstOrDefault(cs => cs.Id == id) ?? new CoreSwitch();
            return coreSwitch.GetModel();
        }

        public bool Create(CoreSwitchModel model)
        {

            bool result = DoAction(delegate ()
            {
                CoreSwitch coreSwitch = new CoreSwitch(model);
                _db.CoreSwitch.Add(coreSwitch);
                _db.SaveChanges();

            });
            return result;
        }

        public bool Delete(int id)
        {
            bool result = DoAction(delegate ()
            {
                CoreSwitch coreSwitch = _db.CoreSwitch.FirstOrDefault(cs => cs.Id == id) ?? new CoreSwitch();


                _db.CoreSwitch.Remove(coreSwitch);

                _db.SaveChanges();
            });
            return result;
        }

       

        public bool Update(int id, CoreSwitchModel model)
        {
            bool result = DoAction(delegate ()
            {
                CoreSwitch coreSwitch = _db.CoreSwitch.FirstOrDefault(cs => cs.Id == id) ?? new CoreSwitch();

                coreSwitch.IPMask = model.IPMask;
                coreSwitch.MACAddress = model.MACAddress;
                coreSwitch.Description= model.Description;
                coreSwitch.Status = model.Status;

                _db.CoreSwitch.Update(coreSwitch);
                _db.SaveChanges();
            });
            return result;
        }

        public void AddPostalAddressToCore(int id, int addressId)
        {
            CoreSwitch coreSwitch = _db.CoreSwitch.FirstOrDefault(cs => cs.Id == id) ?? new CoreSwitch();
            var address = _db.EquipmentManufacturer.FirstOrDefault(e => e.Id == addressId) ?? new EquipmentManufacturer();
            if (coreSwitch.PostalAddressId != address.Id)
            {
                coreSwitch.PostalAddressId = address.Id;
                _db.CoreSwitch.Update(coreSwitch);
            }
            _db.SaveChanges();
        }

        public void AddEquipmentToCore(int id, int equipmentId)
        {
            CoreSwitch coreSwitch = _db.CoreSwitch.FirstOrDefault(cs => cs.Id == id) ?? new CoreSwitch();
            var equipment = _db.EquipmentManufacturer.FirstOrDefault(e => e.Id == equipmentId) ?? new EquipmentManufacturer();
            if (coreSwitch.EquipmentManufacturerId != equipment.Id )
            {
                coreSwitch.EquipmentManufacturerId = equipment.Id;
                _db.CoreSwitch.Update(coreSwitch);
            }
            _db.SaveChanges();
        }
    }
}
