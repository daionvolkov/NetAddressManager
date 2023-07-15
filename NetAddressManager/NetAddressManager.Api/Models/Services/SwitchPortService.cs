using NetAddressManager.Api.Models.Abstractions;
using NetAddressManager.Models;

namespace NetAddressManager.Api.Models.Services
{
    public class SwitchPortService : AbstractionService, ICommonService<SwitchPortModel>
    {

        private readonly ApplicationContext _db;

        public SwitchPortService(ApplicationContext db)
        {
            _db = db;
        }

        public SwitchPortModel Get(int id)
        {
            SwitchPort switchPort = _db.SwitchPort.FirstOrDefault(u => u.Id == id) ?? new SwitchPort();
            return switchPort.GetModel();
        }


        public bool Create(SwitchPortModel model)
        {
            bool result = DoAction(delegate ()
            {
                SwitchPort switchPort = new SwitchPort(model);
                _db.SwitchPort.Add(switchPort);
                _db.SaveChanges();
            });
            return result;
        }

        public bool Delete(int id)
        {
            bool result = DoAction(delegate ()
            {
                SwitchPort switchPort = _db.SwitchPort.FirstOrDefault(x => x.Id == id) ?? new SwitchPort();
                _db.SwitchPort.Remove(switchPort);
                _db.SaveChanges();
            });
            return result;
        }

        public bool Update(int id, SwitchPortModel model)
        {
            bool result = DoAction(delegate ()
            {
                SwitchPort switchPort= _db.SwitchPort.FirstOrDefault(d => d.Id == id) ?? new SwitchPort();

                switchPort.Description = model.Description;
                switchPort.Status= model.Status;

                _db.SwitchPort.Update(switchPort);
                _db.SaveChanges();
            });
            return result;
        }
    }
}
