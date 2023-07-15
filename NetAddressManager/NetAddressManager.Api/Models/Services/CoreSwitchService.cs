using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetAddressManager.Api.Models.Abstractions;
using NetAddressManager.Api.Models.Enums;
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
            CoreSwitch coreSwitch = _db.CoreSwitch
                .Include(cs=>cs.AggregationSwitches)
                .Include(cs=>cs.SwitchPorts)
                .FirstOrDefault(cs => cs.Id == id) ?? new CoreSwitch();
            var coreSwitchModel = coreSwitch?.GetModel();
            if (coreSwitchModel != null) 
            {
                coreSwitchModel.AggregationSwitchIds = coreSwitch.AggregationSwitches.Select(cs => cs.Id).ToList();
                coreSwitchModel.SwitchPortIds = coreSwitch.SwitchPorts.Select(cs => cs.Id).ToList();

            }
            return coreSwitchModel;
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

        public bool AddPostalAddressToCore(int id, int addressId)
        {
            CoreSwitch coreSwitch = _db.CoreSwitch.FirstOrDefault(cs => cs.Id == id) ?? new CoreSwitch();
            bool result = DoAction(delegate ()
            {

                var address = _db.EquipmentManufacturer.FirstOrDefault(e => e.Id == addressId) ?? new EquipmentManufacturer();
                if (coreSwitch.PostalAddressId != address.Id)
                {
                    coreSwitch.PostalAddressId = address.Id;
                    _db.CoreSwitch.Update(coreSwitch);
                    _db.SaveChanges();
                }
                
            });
            return result;
        }

        public bool AddEquipmentToCore(int id, int equipmentId)
        {
            CoreSwitch coreSwitch = _db.CoreSwitch.FirstOrDefault(cs => cs.Id == id) ?? new CoreSwitch();
            bool result = DoAction(delegate ()
            {
                var equipment = _db.EquipmentManufacturer.FirstOrDefault(e => e.Id == equipmentId) ?? new EquipmentManufacturer();
                if (coreSwitch.EquipmentManufacturerId != equipment.Id)
                {
                    coreSwitch.EquipmentManufacturerId = equipment.Id;
                    _db.CoreSwitch.Update(coreSwitch);
                    _db.SaveChanges();
                }
            });
            return result;
        }


        public bool AddAggrSwitchToCore(int id, List<int> aggregationSwitchIds) {
            CoreSwitch coreSwitch = _db.CoreSwitch.FirstOrDefault(cs => cs.Id == id) ?? new CoreSwitch();
            bool result = DoAction(delegate ()
            {
                foreach (int aggregationSwitchId in aggregationSwitchIds)
                {
                    AggregationSwitch aggr = _db.AggregationSwitch.FirstOrDefault(s => s.Id == aggregationSwitchId) ?? new AggregationSwitch();
                    if (coreSwitch.AggregationSwitches.Contains(aggr) == false)
                    {
                        coreSwitch.AggregationSwitches.Add(aggr);
                        _db.CoreSwitch.Update(coreSwitch);
                        _db.SaveChanges();
                    }
                }
            });
            return result;
        }

        public bool AddPortToCore(int id, List<int> portIds)
        {
            CoreSwitch coreSwitch = _db.CoreSwitch.FirstOrDefault(cs => cs.Id == id) ?? new CoreSwitch();
            bool result = DoAction(delegate ()
            {
                foreach (int portId in portIds)
                {
                    SwitchPort switchPort = _db.SwitchPort.FirstOrDefault(p => p.Id == portId) ?? new SwitchPort();
                    if (coreSwitch.SwitchPorts.Contains(switchPort) == false && switchPort.Type == SwitchType.Indeterminate)
                    {
                        coreSwitch.SwitchPorts.Add(switchPort);
                        switchPort.Type = SwitchType.Core;
                        _db.CoreSwitch.Update(coreSwitch);
                        _db.SwitchPort.Update(switchPort);
                    }
                }
                _db.SaveChanges();
            });
            return result;
        }
    }
}
