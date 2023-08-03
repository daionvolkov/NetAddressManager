using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAddressManager.Api.Models.Enums;
using NetAddressManager.Client.Services;
using NetAddressManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NetAddressManager.Client.Services.Tests
{
    [TestClass()]
    public class UserRequestServiceTests
    {
        [TestMethod()]
        public void GetTokenTest()
        {
            var token = new UserRequestService().GetToken("admin", "123");
            Console.WriteLine(token.access_token);
            Assert.IsNotNull(token);

        }


        [TestMethod()]
        public void CreateUserTest()
        {
            var service = new UserRequestService();
            var token = service.GetToken("admin", "123");
            UserModel user = new UserModel("JohnTest", "SmithTest", "123", "johntest@mail.com", "123-123-321", UserStatus.User);
            var result = service.CreateUser(token, user);
            Assert.AreEqual(HttpStatusCode.OK, result);
        }



        [TestMethod()]
        public void GetAllUsersTest()
        {
            var service = new UserRequestService();
            var token = service.GetToken("admin", "123");

            var result = service.GetAllUsers(token);
            Console.WriteLine(result.Count);
            Assert.AreNotEqual(Array.Empty<UserModel>(), result.ToArray());
        }

        [TestMethod()]
        public void DeleteUserTest()
        {
            var service = new UserRequestService();
            var token = service.GetToken("admin", "123");

            var result = service.DeleteUser(token, 6);

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void UpdateUserTest()
        {
            var service = new UserRequestService();
            var token = service.GetToken("admin", "123");
            UserModel user = new UserModel("JohnTest", "SmithTest", "123", "testtest@mail.com", "123-123-321", UserStatus.User);
            user.Id = 6;
            var result = service.UpdateUser(token, user);
            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void GetUserByEmailTest()
        {
            var service = new UserRequestService();
            var token = service.GetToken("admin", "123");
            string email = "testtest@mail.com";
            var result = service.GetUserByEmail(token, email);
            Console.WriteLine(result);
            Assert.AreNotEqual(null, result);
        }
    }
}