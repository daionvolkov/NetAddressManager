using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetAddressManager.Api.Models;
using NetAddressManager.Api.Models.Services;
using NetAddressManager.Models;

namespace NetAddressManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AddressSearchController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private readonly AddressSearchService _addressSearchService;

        public AddressSearchController(ApplicationContext db)
        {
            _db = db;
            _addressSearchService = new AddressSearchService(db);
        }


        [HttpGet("{address}")]
        public async Task<IActionResult> GetAddressData(string address)
        {
            var addressData = await _addressSearchService.GetAddressDataAsync(address);

            if (addressData == null)
            {
                return NotFound("Address data not found");
            }

            return Ok(addressData);
        }


    }
}
