using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetAddressManager.Api.Models;
using NetAddressManager.Api.Models.Services;
using NetAddressManager.Models;

namespace NetAddressManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessSwitchController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private readonly AccessSwitchService _accessSwitchService;
        private readonly CheckDataService _checkDataService;

        public AccessSwitchController(ApplicationContext db)
        {
            _db = db;
            _accessSwitchService = new AccessSwitchService(db);
            _checkDataService = new CheckDataService(db);
        }

        [HttpGet]
        public async Task<IEnumerable<AccessSwitchModel>> GetAccessSwitches()
        {
            return await _db.AccessSwitch.Select(cs => cs.GetModel()).ToListAsync();
        }

        [HttpGet("{id}")]
        public ActionResult<AccessSwitchModel> GetAccessSwitchById(int id)
        {
            var address = _accessSwitchService.Get(id);
            return address == null ? NotFound() : Ok(address);
        }

        [HttpPost]
        public IActionResult CreateAccessSwitch([FromBody] AccessSwitchModel accessSwitchModel)
        {
            if (accessSwitchModel != null)
            {
                bool isIPAddressExists = _checkDataService.CheckIpAddress(accessSwitchModel.IPAddress);
                if (!isIPAddressExists)
                {
                    bool result = _accessSwitchService.Create(accessSwitchModel);
                    return result ? Ok() : NotFound();
                }
                return BadRequest("Устройство с такисм IP-адресом уже существует");
            }
            return BadRequest();
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateAccessSwitch(int id, [FromBody] AccessSwitchModel accessSwitchModel)
        {
            if (accessSwitchModel != null)
            {
                bool result = _accessSwitchService.Update(id, accessSwitchModel);
                return result ? Ok() : NotFound();
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAccessSwitch(int id)
        {
            bool result = _accessSwitchService.Delete(id);
            return result ? Ok() : NotFound();
        }

        [HttpPatch("{id}/address")]
        public IActionResult AddPostalAddressToAccessSwitch(int id, [FromBody] int addressId)
        {
            if (addressId != 0)
            {

                _accessSwitchService.AddPostalAddressToAccess(id, addressId);
                return Ok(addressId);

            }
            return BadRequest();
        }


        [HttpPatch("{id}/equipment")]
        public IActionResult AddEquipmentToAccessSwitch(int id, [FromBody] int equipmentId)
        {
            if (equipmentId != 0)
            {

                _accessSwitchService.AddEquipmentToAccess(id, equipmentId);
                return Ok();

            }
            return BadRequest();
        }

        [HttpPatch("{id}/gateway")]
        public IActionResult AddGatemayToAcceessSwitch(int id, [FromBody] int gatewayId)
        {
            if (gatewayId != 0)
            {

                _accessSwitchService.AddGatewayToAccess(id, gatewayId);
                return Ok();

            }
            return BadRequest();
        }

    }
}
