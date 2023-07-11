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

        public EquipmentManufacturer(EquipmentManufacturerModel equipmentManufacturerModel)
        {
            Manufacturer = equipmentManufacturerModel.Manufacturer;
            Model = equipmentManufacturerModel.Model;
        }

        public EquipmentManufacturer GetModel()
        {
            return new EquipmentManufacturer()
            {
                Id = this.Id,
                Manufacturer = this.Manufacturer,
                Model = this.Model
            };
        }

    }
}
