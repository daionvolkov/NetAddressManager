using Microsoft.EntityFrameworkCore;
using NetAddressManager.Api.Models.Abstractions;
using NetAddressManager.Api.Models.Enums;
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
                .Include(asw=>asw.AccessSwitches)
                .Include(asw=>asw.SwitchPorts)
                .FirstOrDefault(asw=>asw.Id==id) ?? new AggregationSwitch();

            var aggregationSwitchModel = aggregationSwitch?.GetModel();
            if(aggregationSwitchModel != null)
            {
                aggregationSwitchModel.AccessSwitchIds = aggregationSwitch.AccessSwitches.Select(asw => asw.Id).ToList();
                aggregationSwitchModel.SwitchPortIds = aggregationSwitch.SwitchPorts.Select(cs => cs.Id).ToList();
            }
                return aggregationSwitchModel;
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
            if (aggregationSwitch.CoreSwitchId != gateway.Id)
            {
                aggregationSwitch.CoreSwitchId = gateway.Id;
                _db.AggregationSwitch.Update(aggregationSwitch);
            }
            _db.SaveChanges();
        }

        public void AddAccessSwitchToAggregation(int id, List<int> accessSwitchIds)
        {
            AggregationSwitch aggregationSwitch = _db.AggregationSwitch.FirstOrDefault(cs => cs.Id == id) ?? new AggregationSwitch();

            foreach (int accessSwitchId in accessSwitchIds)
            {
                AccessSwitch accessSwitch = _db.AccessSwitch.FirstOrDefault(s => s.Id == accessSwitchId) ?? new AccessSwitch();
                if (aggregationSwitch.AccessSwitches.Contains(accessSwitch) == false)
                {
                    aggregationSwitch.AccessSwitches.Add(accessSwitch);
                    _db.AggregationSwitch.Update(aggregationSwitch);
                }
            }

            _db.SaveChanges();
        }

        public bool AddPortToAggregation(int id, List<int> portIds)
        {
            AggregationSwitch aggregationSwitch = _db.AggregationSwitch.FirstOrDefault(asw => asw.Id == id) ?? new AggregationSwitch();
            bool result = DoAction(delegate ()
            {
                foreach (int portId in portIds)
                {
                    SwitchPort switchPort = _db.SwitchPort.FirstOrDefault(p => p.Id == portId) ?? new SwitchPort();

                    if (aggregationSwitch.SwitchPorts.Contains(switchPort) == false && switchPort.Type == SwitchType.Indeterminate)
                    {
                        aggregationSwitch.SwitchPorts.Add(switchPort);
                        switchPort.Type = SwitchType.Aggregation;
                        _db.AggregationSwitch.Update(aggregationSwitch);
                        _db.SwitchPort.Update(switchPort);
                        _db.SaveChanges();

                    }
                }

            });
            return result;
        }
    }
}
