using NetAddressManager.Api.Models.Abstractions;
using NetAddressManager.Models;

namespace NetAddressManager.Api.Models.Services
{
    public class AggregationSwitchService : AbstractionService, ICommonService<AggregationSwitchModel>
    {
        private readonly ApplicationContext _db;

        public AggregationSwitchService(ApplicationContext db)
        {
            _db = db;
        }

        public AggregationSwitchModel Get(int id)
        {
            AggregationSwitch aggregationSwitch = _db.AggregationSwitch
                .FirstOrDefault(asw=>asw.Id==id) ?? new AggregationSwitch();
            return aggregationSwitch.GetModel();
        }

        public bool Create(AggregationSwitchModel model)
        {
            bool result = DoAction(delegate ()
            {
                AggregationSwitch aggregationSwitch = new AggregationSwitch(model);
                _db.AggregationSwitch.Add(aggregationSwitch);
                _db.SaveChanges();
            });
            return result;
        }

        public bool Delete(int id)
        {
            bool result = DoAction(delegate ()
            {
                AggregationSwitch aggregationSwitch = _db.AggregationSwitch
                .FirstOrDefault(asw => asw.Id == id) ?? new AggregationSwitch();
                _db.AggregationSwitch.Remove(aggregationSwitch);
                _db.SaveChanges();
            });
            return result;
        }

    

        public bool Update(int id, AggregationSwitchModel model)
        {
            bool result = DoAction(delegate ()
            {
                AggregationSwitch aggregationSwitch = _db.AggregationSwitch.FirstOrDefault(asw => asw.Id == id) ?? new AggregationSwitch();

                aggregationSwitch.IPAddress = model.IPAddress;
                aggregationSwitch.IPMask = model.IPMask;
                aggregationSwitch.MACAddress = model.MACAddress;
                aggregationSwitch.Description = model.Description;
                aggregationSwitch.Status = model.Status;

                _db.AggregationSwitch.Update(aggregationSwitch);
                _db.SaveChanges();
            });
            return result;
        }

        public void AddPostalAddressToAggregation(int id, int addressId)
        {
            AggregationSwitch aggregationSwitch = _db.AggregationSwitch.FirstOrDefault(asw => asw.Id == id) ?? new AggregationSwitch();
            var address = _db.EquipmentManufacturer.FirstOrDefault(e => e.Id == addressId) ?? new EquipmentManufacturer();
            if (aggregationSwitch.PostalAddressId != address.Id)
            {
                aggregationSwitch.PostalAddressId = address.Id;
                _db.AggregationSwitch.Update(aggregationSwitch);
            }
            _db.SaveChanges();
        }

        public void AddEquipmentToAggregation(int id, int equipmentId)
        {
            AggregationSwitch aggregationSwitch = _db.AggregationSwitch.FirstOrDefault(asw => asw.Id == id) ?? new AggregationSwitch();
            var equipment = _db.EquipmentManufacturer.FirstOrDefault(e => e.Id == equipmentId) ?? new EquipmentManufacturer();
            if (aggregationSwitch.EquipmentManufacturerId != equipment.Id)
            {
                aggregationSwitch.EquipmentManufacturerId = equipment.Id;
                _db.AggregationSwitch.Update(aggregationSwitch);
            }
            _db.SaveChanges();
        }

        public void AddGatewayToAggregation(int id, int gatewayId)
        {
            AggregationSwitch aggregationSwitch = _db.AggregationSwitch.FirstOrDefault(asw => asw.Id == id) ?? new AggregationSwitch();
            var gateway = _db.CoreSwitch.FirstOrDefault(g=>g.Id == gatewayId) ?? new CoreSwitch();
            if(aggregationSwitch.CoreSwitchId != gateway.Id)
            {
                aggregationSwitch.CoreSwitchId = gateway.Id;
                _db.AggregationSwitch.Update(aggregationSwitch);
            }
            _db.SaveChanges();
        }
    }
}
