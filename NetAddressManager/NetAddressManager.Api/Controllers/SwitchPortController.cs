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
    public class SwitchPortController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private readonly SwitchPortService _switchPortService;

        public SwitchPortController(ApplicationContext db)
        {
            _db = db;
            _switchPortService = new SwitchPortService(db);
        }

        [HttpGet]
        public async Task<IEnumerable<SwitchPortModel>> GetSwitchPorts()
        {
            return await _db.SwitchPort.Select(pa => pa.GetModel()).ToListAsync();
        }


        [HttpGet("{id}")]
        public ActionResult<SwitchPortModel> GetSwitchPortById(int id)
        {
            var address = _switchPortService.Get(id);
            return address == null ? NotFound() : Ok(address);
        }


        [HttpPost]
        public IActionResult CreateSwitchPort([FromBody] SwitchPortModel switchPortModel)
        {
            if (switchPortModel != null)
            {
                bool result = _switchPortService.Create(switchPortModel);
                return result ? Ok() : NotFound();
            }
            return BadRequest();
        }

        [HttpPatch("{id}")]
        public IActionResult UpddateSwitchPort(int id, [FromBody] SwitchPortModel switchPortModel)
        {
            if (switchPortModel != null)
            {
                bool result = _switchPortService.Update(id, switchPortModel);
                return result ? Ok() : NotFound();
            }
            return BadRequest();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteSwitchPort(int id)
        {
            bool result = _switchPortService.Delete(id);
            return result ? Ok() : NotFound();
        }
    }
}
