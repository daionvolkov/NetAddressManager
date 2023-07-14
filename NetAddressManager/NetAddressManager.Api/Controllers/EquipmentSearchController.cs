using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetAddressManager.Api.Models;
using NetAddressManager.Api.Models.Services;

namespace NetAddressManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentSearchController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private readonly EquipmentSearchService _equipmentSearchService;

        public EquipmentSearchController(ApplicationContext db)
        {
            _db = db;
            _equipmentSearchService = new EquipmentSearchService(db);
        }

        [HttpGet("{equipment}")]
        public async Task<IActionResult> GetAddressData(string equipment)
        {
            var equipmentData = await _equipmentSearchService.GetEquipmentDataAsync(equipment);

            if (equipmentData == null)
            {
                return NotFound("Equipment not found");
            }

            return Ok(equipmentData);
        }
    }
}
