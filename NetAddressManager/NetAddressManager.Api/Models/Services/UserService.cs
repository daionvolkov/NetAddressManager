using NetAddressManager.Api.Models.Abstractions;
using NetAddressManager.Models;
using System.Diagnostics.Eventing.Reader;
using System.Security.Claims;
using System.Text;

namespace NetAddressManager.Api.Models.Services
{
    public class UserService : AbstractionService, ICommonService<UserModel>
    {
        private ApplicationContext _db;
        public UserService(ApplicationContext db)
        {
            _db = db;
        }

        public Tuple<string, string> GetUserLoginPassFromBasicAuth(HttpRequest request)
        {
            string userName = "";
            string userPass = "";
            string authHeader = request.Headers["Authorization"].ToString();

            if (authHeader != null && authHeader.StartsWith("Basic"))
            {
                string encodedUserNamePass = authHeader.Replace("Basic ", "");
                var encoding = Encoding.GetEncoding("iso-8859-1");

                string[] namePassArray = encoding.GetString(Convert.FromBase64String(encodedUserNamePass)).Split(':');
                userName = namePassArray[0];
                userPass = namePassArray[1];
            }
            return new Tuple<string, string>(userName, userPass);
        }      

        public User GetUser(string login, string password)
        {
            var user = _db.User.FirstOrDefault(u => u.Email == login && u.Password == password);
            return user;
        }
        public User GetUser(string login)
        {
            var user = _db.User.FirstOrDefault(u => u.Email == login) ?? new User();
            return user;
        }

        public ClaimsIdentity? GetIdentity(string username, string password)
        {
            User currentUser = GetUser(username, password);
            if (currentUser != null)
            {
                currentUser.LastLoginDate = DateTime.Now;
                _db.User.Update(currentUser);
                _db.SaveChanges();

                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, currentUser.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, currentUser.Status.ToString())
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            return null;
            
        }



        public bool Create(UserModel model)
        {
            return DoAction(delegate ()
            {
                User user = new User(model.FirstName, model.LastName,  model.Password, model.Email, model.Phone, model.Status);
                _db.User.Add(user); 
                _db.SaveChanges();
            });

        }

        public bool Delete(int id)
        {
            User user = _db.User.FirstOrDefault(u => u.Id == id) ?? new User();
            if (user != null)
            {
                return DoAction(delegate ()
                {
                    _db.User.Remove(user);
                    _db.SaveChanges();
                });
            }
            return false;

        }

        public UserModel Get(int id)
        {
            User user = _db.User.FirstOrDefault(u => u.Id == id) ?? new User();
            return user.GetModel();

        }

        public bool Update(int id, UserModel model)
        {
            User user = _db.User.FirstOrDefault(u => u.Id == id) ?? new User();
            if (user != null) { }
            {
                return DoAction(delegate ()
                {
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.Password = model.Password;
                    user.Phone = model.Phone;
                    user.Status = model.Status;
                    user.Email = model.Email;
                    _db.User.Update(user);
                    _db.SaveChanges();
                });
            }

        }
    }
}
