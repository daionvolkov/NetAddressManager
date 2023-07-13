using NetAddressManager.Api.Models.Abstractions;
using NetAddressManager.Models;

namespace NetAddressManager.Api.Models.Services
{
    public class PostalAddressService : AbstractionService, ICommonService<PostalAddressModel>
    {
        private readonly ApplicationContext _db;
        public PostalAddressService(ApplicationContext db)
        {
            _db = db;
        }


        public PostalAddressModel Get(int id)
        {
            PostalAddress address = _db.PostalAddress.FirstOrDefault(u => u.Id == id) ?? new PostalAddress();
            return address.GetModel();
        }


        public bool Create(PostalAddressModel model)
        {
            bool result = DoAction(delegate ()
            {
                PostalAddress address = new PostalAddress(model);
                _db.PostalAddress.Add(address);
                _db.SaveChanges();
            });
            return result;
        }

        public bool Delete(int id)
        {
            bool result = DoAction(delegate ()
            {
                PostalAddress address = _db.PostalAddress.FirstOrDefault(x => x.Id == id) ?? new PostalAddress();
                _db.PostalAddress.Remove(address);
                _db.SaveChanges();
            });
            return result;
        }



        public bool Update(int id, PostalAddressModel model)
        {
            bool result = DoAction(delegate ()
            {
                PostalAddress address = _db.PostalAddress.FirstOrDefault(d => d.Id == id) ?? new PostalAddress();

                address.City = model.City;
                address.Street = model.Street;
                address.Building = model.Building;

                _db.PostalAddress.Update(address);
                _db.SaveChanges();
            });
            return result;
        }
    }
}
