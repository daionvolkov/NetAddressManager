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
    public class AggregationSwitchRequestServiceTests
    {
        private AuthToken _token;
        private AggregationSwitchRequestService _service;

        public AggregationSwitchRequestServiceTests()
        {
            _token = new UserRequestService().GetToken("admin", "123");
            _service = new AggregationSwitchRequestService();
        }

        [TestMethod()]
        public void GetAllAggregationSwitchesTest()
        {
            var result = _service.GetAllAggregationSwitches(_token);
            Console.WriteLine(result.Count);
            Assert.AreNotEqual(Array.Empty<CoreSwitchModel>(), result.ToArray());
        }

        [TestMethod()]
        public void GetAggregationSwitchByIdTest()
        {
            var result = _service.GetAggregationSwitchById(_token, 1);
            Assert.AreNotEqual(null, result);
        }

        [TestMethod()]
        public void CreateAggregationSwitchTest()
        {
            AggregationSwitchModel model = new AggregationSwitchModel("15.15.15.12", "255.255.255.0", "A1:A2:A3:A4:A5:A7", "test AggregationSwitch", SwitchStatus.Inaccessible);

            var result = _service.CreateAggregationSwitch(_token, model);
            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void UpdateAggregationSwitchTest()
        {
            AggregationSwitchModel model = new AggregationSwitchModel("15.15.15.12", "255.255.255.0", "A1:A2:A3:A4:A5:A7", "update test AggregationSwitch", SwitchStatus.Inaccessible);

            model.Id = 16;
            var result = _service.UpdateAggregationSwitch(_token, model);
            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void DeleteAggregationSwitchTest()
        {
            var result = _service.DeleteAggregationSwitch(_token, 16);
            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void AddPostalAddressToAggregationSwitchTest()
        {
            var result = _service.AddPostalAddressToAggregationSwitch(_token, 1, 5);
            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void AddEquipmentToAggregationSwitchTest()
        {
            var result = _service.AddEquipmentToAggregationSwitch(_token, 1, 5);
            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void AddPortsToAggregationSwitchTest()
        {
            var result = _service.AddPortsToAggregationSwitch(_token, 1, new List<int>() { 7, 8 });
            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void AddAccessSwitchToAggregationSwitchTest()
        {
            var result = _service.AddAccessSwitchToAggregationSwitch(_token, 1, new List<int>() { 7, 8 });
            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void RemoveAccessSwitchFromAggregationSwitchTest()
        {
            var result = _service.RemoveAccessSwitchFromAggregationSwitch(_token, 1, new List<int>() { 7, 8 });
            Assert.AreEqual(HttpStatusCode.OK, result);
        }
    }
}