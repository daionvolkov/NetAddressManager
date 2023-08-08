using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetAddressManager.Api.Models;
using NetAddressManager.Api.Models.Services;
using NetAddressManager.Models;
using System.IO;

namespace NetAddressManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckDataController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private readonly CheckDataService _checkDataService;

        public CheckDataController(ApplicationContext db)
        {
            _db = db;
            _checkDataService = new CheckDataService(db);
        }

        [HttpGet("{address}/address")]
        public ActionResult<PostalAddressModel> IsPostalAddressExists(string address)
        {
            var postalAddress = address.Split(',');
            var result = _checkDataService.GetPostalAddressId(postalAddress[0].Trim(), postalAddress[1].Trim(), postalAddress[2].Trim());
            
            return result == null ? NotFound() : Ok(result);

        }

        [HttpGet("{equipment}/equipment")]
        public IActionResult IsEquipmentExists(string equipment)
        {
            var equipmentManufacturer = equipment.Split(',');
            bool isExists = _checkDataService.CheckEquipment(equipmentManufacturer[0].Trim(), equipmentManufacturer[1].Trim());

            return isExists ? Ok(isExists) : BadRequest(!isExists);
        }

        [HttpGet("{netAddress}/netAddress")]
        public IActionResult IsIpAddressExists(string ipAddress)
        {
            bool isExists = _checkDataService.CheckIpAddress(ipAddress);

            return isExists ? Ok(isExists) : BadRequest(!isExists);
        }
    }
}
