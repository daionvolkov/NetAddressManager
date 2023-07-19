using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAddressManager.Api.Models.Enums;
using NetAddressManager.Client.Models;
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
    public class PostalAddressRequestServiceTests
    {
        private AuthToken _token;
        private PostalAddressRequestService _service;

        public PostalAddressRequestServiceTests()
        {
            _token = new UserRequestService().GetToken("admin", "123");
            _service = new PostalAddressRequestService();
        }

        [TestMethod()]
        public void GetAllPostalAddressesTest()
        {
            var result = _service.GetAllPostalAddresses(_token);
            Assert.AreNotEqual(Array.Empty<PostalAddressModel>(), result.ToArray());
        }

        [TestMethod()]
        public void GetPostalAddressByIdTest()
        {
            var result = _service.GetPostalAddressById(_token, 1);
            Assert.AreNotEqual(null, result);
        }

        [TestMethod()]
        public void CreatePostalAddressTest()
        {
            PostalAddressModel model = new PostalAddressModel("Moscow", "Arbat", "123");
            var result = _service.CreatePostalAddress(_token, model);
            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void UpdatePostalAddressTest()
        {
            PostalAddressModel model = new PostalAddressModel("Moscow", "Arbat", "321");
            model.Id = 11;
            var result = _service.UpdatePostalAddress(_token, model);
            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void DeletePostalAddressTest()
        {
            var result = _service.DeletePostalAddress(_token, 11);
            Assert.AreEqual(HttpStatusCode.OK, result);
        }
    }
}