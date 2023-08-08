using NetAddressManager.Models;

namespace NetAddressManager.Api.Models.Services
{
    public class CheckDataService
    {
        private readonly ApplicationContext _db;

        public CheckDataService(ApplicationContext db)
        {
            _db = db;
        }

        public bool CheckIpAddress(string ipAddress)
        {
            bool ipAddressExists = false;

            ipAddressExists = _db.CoreSwitch.Any(s => s.IPAddress == ipAddress)
                || _db.AggregationSwitch.Any(s => s.IPAddress == ipAddress)
                || _db.AccessSwitch.Any(s => s.IPAddress == ipAddress);

            return ipAddressExists;
        }

        public bool CheckPostalAddress(string city, string street, string building)
        {
            bool isPostalAddressExists = false;
            isPostalAddressExists = _db.PostalAddress.Any(pa => (pa.City == city) && (pa.Street == street) && (pa.Building == building));
            return isPostalAddressExists;
        }

        public PostalAddressModel GetPostalAddressId(string city, string street, string building)
        {
            PostalAddressModel postalAddress = _db.PostalAddress.
                FirstOrDefault(pa => (pa.City == city) && (pa.Street == street) && (pa.Building == building)).GetModel();
            return postalAddress;
        }

        public bool CheckEquipment(string manufacturer, string model) 
        {
            bool isEquipmentExists = false;
            isEquipmentExists = _db.EquipmentManufacturer.Any(e=>(e.Manufacturer == manufacturer) && (e.Model == model));
            return isEquipmentExists;
        }
    }
}
