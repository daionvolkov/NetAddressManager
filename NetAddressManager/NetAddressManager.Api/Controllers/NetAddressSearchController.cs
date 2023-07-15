using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetAddressManager.Api.Models;
using NetAddressManager.Api.Models.Services.SearchServices;

namespace NetAddressManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NetAddressSearchController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private readonly NetAddressSearchService _netAddressSearchService;

        public NetAddressSearchController(ApplicationContext db)
        {
            _db = db;
            _netAddressSearchService = new NetAddressSearchService(db);
        }


        [HttpGet("{ipaddress}")]
        public async Task<IActionResult> GetNetAddrressData(string ipaddress)
        {
            var switchData = await _netAddressSearchService.SearchSwitchByIpAddressAsync(ipaddress);
            if (switchData == null)
            {
                return NotFound("Device not found");
            }
            return Ok(switchData);
        }
    }
}
