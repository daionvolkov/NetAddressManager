using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAddressManager.Client.Models;
using NetAddressManager.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetAddressManager.Client.Services.Tests
{
    [TestClass()]
    public class EquipmentSearchRequestServiceTests
    {
        private AuthToken _token;
        private EquipmentSearchRequestService _service;

        public EquipmentSearchRequestServiceTests()
        {
            _token = new UserRequestService().GetToken("admin", "123");
            _service = new EquipmentSearchRequestService();
        }

        [TestMethod()]
        public void GetSwitchesByEquipmentTest()
        {
            var result = _service.GetSwitchesByEquipment(_token, "2200");
            Console.WriteLine(result);
            Assert.AreNotEqual(null, result);
        }
    }
}