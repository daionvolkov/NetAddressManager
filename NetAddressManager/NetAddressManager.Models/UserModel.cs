using NetAddressManager.Api.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetAddressManager.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public UserStatus Status { get; set; }

        public UserModel() { }
        public UserModel(string firstName, string lastName, string password, string email, string phone=null,  UserStatus status = UserStatus.User)
        {
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            Email = email;
            Phone = phone;
            RegistrationDate = DateTime.Now;
            Status = status;
        }
    }
}
