using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAddressManager.Client.Models;
using NetAddressManager.Client.Services;
using NetAddressManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetAddressManager.Client.Services.Tests
{
    [TestClass()]
    public class AddressSearchRequestServiceTests
    {
        private AuthToken _token;
        private AddressSearchRequestService _service;

        public AddressSearchRequestServiceTests()
        {
            _token = new UserRequestService().GetToken("admin", "123");
            _service = new AddressSearchRequestService();
        }

        [TestMethod()]
        public void GetAllAccessSwitchesTest()
        {
            var result = _service.GetSwitchesByAddress(_token, "New York");
            Console.WriteLine(result);
            Assert.AreNotEqual(null, result);
        }
    }
}