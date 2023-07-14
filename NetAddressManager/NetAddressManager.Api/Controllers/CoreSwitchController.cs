﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetAddressManager.Api.Models;
using NetAddressManager.Api.Models.Enums;
using NetAddressManager.Api.Models.Services;
using NetAddressManager.Models;

namespace NetAddressManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CoreSwitchController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private readonly CoreSwitchService _coreSwitchService;
        private readonly CheckDataService _checkDataService;

        public CoreSwitchController(ApplicationContext db)
        {
            _db = db;
            _coreSwitchService = new CoreSwitchService(db);
            _checkDataService = new CheckDataService(db);
        }

        [HttpGet]
        public async Task<IEnumerable<CoreSwitchModel>> GetCoreSwitches()
        {
            return await _db.CoreSwitch.Select(cs => cs.GetModel()).ToListAsync();
        }

        [HttpGet("{id}")]
        public ActionResult<CoreSwitchModel> GetCoreSwitchById(int id)
        {
            var address = _coreSwitchService.Get(id);
            return address == null ? NotFound() : Ok(address);
        }
        
        [HttpPost]
        public IActionResult CreateCoreSwitch([FromBody] CoreSwitchModel coreSwitchModel)
        {
            
            if (coreSwitchModel != null)
            {
                bool isIPAddressExists = _checkDataService.CheckIpAddress(coreSwitchModel.IPAddress);
                if (!isIPAddressExists)
                {
                    bool result = _coreSwitchService.Create(coreSwitchModel);
                    return result ? Ok() : NotFound();
                }
                return BadRequest("Устройство с такисм IP-адресом уже существует");
            }
            return BadRequest();
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateCoreSwitch(int id, [FromBody] CoreSwitchModel coreSwitchModel)
        {
            if (coreSwitchModel != null)
            {
                bool result = _coreSwitchService.Update(id, coreSwitchModel);
                return result ? Ok() : NotFound();
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCoreSwitch(int id)
        {
            bool result = _coreSwitchService.Delete(id);
            return result ? Ok() : NotFound();
        }

        [HttpPatch("{id}/address")]
        public IActionResult AddPostalAddressToCoreSwitch(int id, [FromBody]  int addressId)
        {
            if (addressId != 0)
            {
                _coreSwitchService.AddPostalAddressToCore(id, addressId);
                return Ok(addressId);
            }
            return BadRequest();
        }
        [HttpPatch("{id}/equipment")]
        public IActionResult AddEquipmentToCoreSwitch(int id, [FromBody] int equipmentId)
        {
            if (equipmentId != 0)
            {
                _coreSwitchService.AddEquipmentToCore(id, equipmentId);
            }
            return BadRequest();
        }


    }
}
