using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAddressManager.Api.Models.Enums;
using NetAddressManager.Client.Models;
using NetAddressManager.Client.Services;
using NetAddressManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NetAddressManager.Client.Services.Tests
{
    [TestClass()]
    public class CoreSwitchRequestServiceTests
    {
        private AuthToken _token;
        private CoreSwitchRequestService _service;
        public CoreSwitchRequestServiceTests()
        {
            _token = new UserRequestService().GetToken("admin", "123");
            _service = new CoreSwitchRequestService();
        }

        [TestMethod()]
        public void GetAllCoreSwitchesTest()
        {
            var result = _service.GetAllCoreSwitches(_token);
            Console.WriteLine(result.Count);
            Assert.AreNotEqual(Array.Empty<CoreSwitchModel>(), result.ToArray());
        }


        [TestMethod()]
        public void GetCoreSwitchByIdTest()
        {
            var result = _service.GetCoreSwitchById(_token, 1);
            Assert.AreNotEqual(null, result);
        }


        [TestMethod()]
        public void CreateCoreSwitchTest()
        {
            CoreSwitchModel model = new CoreSwitchModel("10.10.1.12", "255.255.255.0", "A1:A2:A3:A4:A5:A6", "New test CoreSwitch", SwitchStatus.Inaccessible, "1.1.1.1");

            var result = _service.CreateCoreSwitch(_token, model);
            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void UpdateCoreSwitchTest()
        {
            CoreSwitchModel model = new CoreSwitchModel("10.10.1.12", "255.255.255.0", "A1:A2:A3:A4:A5:A6", "Update New test CoreSwitch", SwitchStatus.Inaccessible, "1.1.1.1");
            model.Id = 37;
            var result = _service.UpdateCoreSwitch(_token, model);
            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void DeleteCoreSwitchTest()
        {
            var result = _service.DeleteCoreSwitch(_token, 37);
            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void AddPostalAddressToCoreSwitchTest()
        {
            var result = _service.AddPostalAddressToCoreSwitch(_token, 4, 5);
            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void AddEquipmentToCoreSwitchTest()
        {
            var result = _service.AddEquipmentToCoreSwitch(_token, 4, 6);
            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void AddPortsToCoreSwitchTest()
        {
            var result = _service.AddPortsToCoreSwitch(_token, 4, new List<int>() { 4 });
            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void AddAggregationSwitchToCoreSwitchTest()
        {
            var result = _service.AddAggregationSwitchToCoreSwitch(_token, 5, new List<int>() { 5, 6 });
            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void RemoveAggregationSwitchFromCoreSwitchTest()
        {
            var result = _service.RemoveAggregationSwitchFromCoreSwitch(_token, 5, new List<int>() { 5, 6 });
            Assert.AreEqual(HttpStatusCode.OK, result);
        }
    }
}