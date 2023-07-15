using NetAddressManager.Api.Models.Enums;
using NetAddressManager.Models;

namespace NetAddressManager.Api.Models
{
    public class User
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

        public User() { }
        public User(string firstName, string lastName, string password, string email, string? phone = null, UserStatus status = UserStatus.User)
        {
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            Email = email;
            Phone = phone;
            Status = status;
            RegistrationDate = DateTime.Now;
        }

        public User(UserModel userModel)
        {
            Id = userModel.Id;
            FirstName = userModel.FirstName;
            LastName = userModel.LastName;
            Password = userModel.Password;
            Email = userModel.Email;
            Phone = userModel.Phone;
            RegistrationDate = userModel.RegistrationDate;
            Status = userModel.Status;
        }

        public UserModel GetModel()
        {
            return new UserModel
            {
                Id = this.Id,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Password = this.Password,
                Email = this.Email,
                RegistrationDate = this.RegistrationDate,
                Status = this.Status,
                Phone = this.Phone,
            };
        }
    }
}
