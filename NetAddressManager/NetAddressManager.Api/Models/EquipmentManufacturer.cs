using NetAddressManager.Models;

namespace NetAddressManager.Api.Models
{
    public class EquipmentManufacturer
    {
        public int Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }

        public EquipmentManufacturer()
        {
        }

        public EquipmentManufacturer(int id, string manufacturer, string model)
        {
            Id = id;
            Manufacturer = manufacturer;
            Model = model;
        }

        public EquipmentManufacturer(EquipmentManufacturerModel equipmentManufacturerModel)
        {
            Manufacturer = equipmentManufacturerModel.Manufacturer;
            Model = equipmentManufacturerModel.Model;
        }

        public EquipmentManufacturerModel GetModel()
        {
            return new EquipmentManufacturerModel
            {
                Id = this.Id,
                Manufacturer = this.Manufacturer,
                Model = this.Model
            };
        }

    }
}
