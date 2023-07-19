using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAddressManager.Client.Models;
using NetAddressManager.Client.Services;
using NetAddressManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace NetAddressManager.Client.Services.Tests
{
    [TestClass()]
    public class EquipmentRequestServiceTests
    {
        private AuthToken _token;
        private EquipmentRequestService _service;

        public EquipmentRequestServiceTests()
        {
            _token = new UserRequestService().GetToken("admin", "123");
            _service = new EquipmentRequestService();
        }

        [TestMethod()]
        public void GetAllEquipmentsTest()
        {
            var result = _service.GetAllEquipments(_token);
            Assert.AreNotEqual(Array.Empty<EquipmentManufacturerModel>(), result.ToArray());
        }

        [TestMethod()]
        public void GetEquipmentByIdTest()
        {
            var result = _service.GetEquipmentById(_token, 1);
            Assert.AreNotEqual(null, result);
        }

        [TestMethod()]
        public void CreateEquipmentTest()
        {
            EquipmentManufacturerModel model = new EquipmentManufacturerModel("FiberHome", "S5152-AI");
            var result = _service.CreateEquipment(_token, model);
            Assert.AreEqual(HttpStatusCode.OK, result);

        }

        [TestMethod()]
        public void UpdateEquipmentTest()
        {
            EquipmentManufacturerModel model = new EquipmentManufacturerModel("FiberHome", "S5152-FA");
            model.Id = 19;
            var result = _service.CreateEquipment(_token, model);
            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void DeleteEquipmentTest()
        {
            var result = _service.DeleteEquipment(_token, 19);
            Assert.AreEqual(HttpStatusCode.OK, result);
        }
    }
}