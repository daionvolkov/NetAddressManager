using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class AggregationSwitchController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private readonly AggregationSwitchService _aggregationSwitchService;
        private readonly CheckDataService _checkDataService;

        public AggregationSwitchController(ApplicationContext db)
        {
            _db = db;
            _aggregationSwitchService = new AggregationSwitchService(db);
            _checkDataService = new CheckDataService(db);
        }


        [HttpGet]
        public async Task<IEnumerable<AggregationSwitchModel>> GetAggregationSwitches()
        {
            return await _db.AggregationSwitch.Select(cs => cs.GetModel()).ToListAsync();
        }


        [HttpGet("{id}")]
        public ActionResult<AccessSwitchModel> GetAggregationSwitchById(int id)
        {
            var address = _aggregationSwitchService.Get(id);
            return address == null ? NotFound() : Ok(address);
        }


        [HttpPost]
        public IActionResult CreateAggregationSwitch([FromBody] AggregationSwitchModel aggregationSwitchModel)
        {
            if (aggregationSwitchModel != null)
            {
                bool isIPAddressExists = _checkDataService.CheckIpAddress(aggregationSwitchModel.IPAddress);
                if (!isIPAddressExists)
                {
                    bool result = _aggregationSwitchService.Create(aggregationSwitchModel);
                    return result ? Ok() : NotFound();
                }
                return BadRequest("Устройство с такисм IP-адресом уже существует");
            }
            return BadRequest();
        }


        [HttpPatch("{id}")]
        public IActionResult UpdateAggregationSwitch(int id, [FromBody] AggregationSwitchModel aggregationSwitchModel)
        {
            if (aggregationSwitchModel != null)
            {
                bool result = _aggregationSwitchService.Update(id, aggregationSwitchModel);
                return result ? Ok() : NotFound();
            }
            return BadRequest();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteAggregationSwitch(int id)
        {
            bool result = _aggregationSwitchService.Delete(id);
            return result ? Ok() : NotFound();
        }


        [HttpPatch("{id}/address")]
        public IActionResult AddPostalAddressToAggregationSwitch(int id, [FromBody] int addressId)
        {
            if (addressId != 0)
            {
                bool result = _aggregationSwitchService.AddPostalAddressToAggregation(id, addressId);
                return result ? Ok() : BadRequest("Невозможно добавить");
            }
            return BadRequest();
        }


        [HttpPatch("{id}/equipment")]
        public IActionResult AddEquipmentToAggregationSwitch(int id, [FromBody] int equipmentId)
        {
            if (equipmentId != 0)
            {
                bool result = _aggregationSwitchService.AddEquipmentToAggregation(id, equipmentId);
                return result ? Ok() : BadRequest("Невозможно добавить");
            }
            return BadRequest();
        }

        [HttpPatch("{id}/gateway")]
        public IActionResult AddGatemayToAggregationSwitch(int id, [FromBody] int gatewayId)
        {
            if (gatewayId != 0)
            {
                _aggregationSwitchService.AddGatewayToAggregation(id, gatewayId);
                return Ok();

            }
            return BadRequest();
        }



        [HttpPatch("{id}/accessswitch")]
        public IActionResult AddAccessSwitchToAggregationSwitch(int id, [FromBody] List<int> accessSwitchId)
        {
            if (accessSwitchId != null)
            {
                bool result = _aggregationSwitchService.AddAccessSwitchToAggregation(id, accessSwitchId);
                return result ? Ok() : BadRequest("Невозможно добавить");
            }
            return BadRequest();
        }


        [HttpPatch("{id}/accessswitch/remove")]
        public IActionResult RemoveGatewayFromAggregationSwitch(int id, [FromBody] List<int> gatewayId)
        {
            if (gatewayId != null)
            {
                bool result = _aggregationSwitchService.RemoveGatewayFromAggregation(id, gatewayId);
                return result ? Ok() : BadRequest();
            }
            return BadRequest();
        }


        [HttpPatch("{id}/port")]
        public IActionResult AddSwitchPortAggresgationSwitch(int id, [FromBody] List<int> portIds)
        {
            if (portIds != null)
            {
                bool result = _aggregationSwitchService.AddPortToAggregation(id, portIds);
                return result ? Ok() : BadRequest("Невозможно добавить, порт занят");
            }
            return BadRequest();
        }

    }
}
