using NetAddressManager.Api.Models.Abstractions;
using NetAddressManager.Models;

namespace NetAddressManager.Api.Models.Services
{
    public class EquipmentManufacturerService : AbstractionService, ICommonService<EquipmentManufacturerModel>
    {
        private readonly ApplicationContext _db;

        public EquipmentManufacturerService(ApplicationContext db)
        {
            _db = db;
        }

        public EquipmentManufacturerModel Get(int id)
        {
            EquipmentManufacturer equipment = _db.EquipmentManufacturer.FirstOrDefault(u => u.Id == id) ?? new EquipmentManufacturer();
            return equipment.GetModel();
        }

        public bool Create(EquipmentManufacturerModel model)
        {
            bool result = DoAction(delegate ()
            {
                EquipmentManufacturer equipment = new EquipmentManufacturer(model);
                _db.EquipmentManufacturer.Add(equipment);
                _db.SaveChanges();
            });
            return result;
        }

        public bool Delete(int id)
        {
            bool result = DoAction(delegate ()
            {
                EquipmentManufacturer equipment = _db.EquipmentManufacturer.FirstOrDefault(x => x.Id == id) ?? new EquipmentManufacturer();
                _db.EquipmentManufacturer.Remove(equipment);
                _db.SaveChanges();
            });
            return result;
        }

      

        public bool Update(int id, EquipmentManufacturerModel model)
        {
            bool result = DoAction(delegate ()
            {
                EquipmentManufacturer equipment = _db.EquipmentManufacturer.FirstOrDefault(d => d.Id == id) ?? new EquipmentManufacturer();

                equipment.Manufacturer = model.Manufacturer;
                equipment.Model = model.Model;

                _db.EquipmentManufacturer.Update(equipment);
                _db.SaveChanges();
            });
            return result;
        }
    }
}
