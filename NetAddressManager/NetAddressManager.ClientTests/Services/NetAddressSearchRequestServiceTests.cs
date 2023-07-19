using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAddressManager.Client.Models;
using NetAddressManager.Client.Services;
using NetAddressManager.ClientTests.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetAddressManager.ClientTests.Services.Tests
{
    [TestClass()]
    public class NetAddressSearchRequestServiceTests
    {
        private AuthToken _token;
        private NetAddressSearchRequestService _service;

        public NetAddressSearchRequestServiceTests()
        {
            _token = new UserRequestService().GetToken("admin", "123");
            _service = new NetAddressSearchRequestService();
        }

   

        [TestMethod()]
        public void GetSwitchesByNetAddressTest()
        {
            var result = _service.GetSwitchesByNetAddress(_token, "10.0.0.1");
            Console.WriteLine(result);
            Assert.AreNotEqual(null, result);
        }
    }
}