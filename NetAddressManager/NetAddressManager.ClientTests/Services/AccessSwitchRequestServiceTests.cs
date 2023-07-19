using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAddressManager.Api.Models.Enums;
using NetAddressManager.Client.Models;
using NetAddressManager.Client.Services;
using NetAddressManager.Models;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NetAddressManager.Client.Services.Tests
{
    [TestClass()]
    public class AccessSwitchRequestServiceTests
    {
        private AuthToken _token;
        private AccessSwitchRequestService _service;

        public AccessSwitchRequestServiceTests()
        {
            _token = new UserRequestService().GetToken("admin", "123");
            _service = new AccessSwitchRequestService();
        }

        [TestMethod()]
        public void GetAllAccessSwitchesTest()
        {
            var result = _service.GetAllAccessSwitches(_token);
            Assert.AreNotEqual(Array.Empty<AccessSwitchModel>(), result.ToArray());
        }

        [TestMethod()]
        public void GetAccessSwitchByIdTest()
        {
            var result = _service.GetAccessSwitchById(_token, 1);
            Assert.AreNotEqual(null, result);
        }

        [TestMethod()]
        public void CreateAccessSwitchTest()
        {
            AccessSwitchModel model = new AccessSwitchModel("111.123.12.12", "255.255.255.0", "A1:A2:A3:A4:A5:A7", "test for access switch", SwitchStatus.Inaccessible);
            var result = _service.CreateAccessSwitch(_token, model);
            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void UpdateAccessSwitchTest()
        {
            AccessSwitchModel model = new AccessSwitchModel("111.123.12.12", "255.255.255.0", "A1:A2:A3:A4:A5:A7", "update test for access switch", SwitchStatus.Inaccessible);
            model.Id = 14;
            var result = _service.UpdateAccessSwitch(_token, model);
            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void DeleteAccessSwitchTest()
        {
            var result = _service.DeleteAccessSwitch(_token, 14);
            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void AddPostalAddressToAccessSwitchTest()
        {
            var result = _service.AddPostalAddressToAccessSwitch(_token, 5, 5);
            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void AddEquipmentToAccessSwitchTest()
        {
            var result = _service.AddEquipmentToAccessSwitch(_token, 5, 5);
            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void AddPortsToAccessSwitchTest()
        {
            var result = _service.AddPortsToAccessSwitch(_token, 5, new List<int>() { 9,10 });
            Assert.AreEqual(HttpStatusCode.OK, result);
        }
    }
}