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
    public class EquipmentController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private readonly EquipmentManufacturerService _equipmentManufacturerService;
        private readonly CheckDataService _checkDataService;
        
        public EquipmentController(ApplicationContext db)
        {
            _db = db;
            _equipmentManufacturerService = new EquipmentManufacturerService(db);
            _checkDataService = new CheckDataService(db);
        }
        

        [HttpGet]
        public async Task<IEnumerable<EquipmentManufacturerModel>> GetEquipment()
        {
            return await _db.EquipmentManufacturer.Select(u => u.GetModel()).ToListAsync();
        }

        [HttpGet("{id}")]
        public ActionResult<EquipmentManufacturerModel> GetEquipmentManufacturerById(int id)
        {
            var equipment = _equipmentManufacturerService.Get(id);
            return equipment == null ? NotFound() : Ok(equipment);
        }


        [HttpPost]
        public IActionResult CreateEquipmentManufacturer([FromBody] EquipmentManufacturerModel equipmentManufacturerModel)
        {
            if (equipmentManufacturerModel != null)
            {
                bool isEquipmentExists = _checkDataService.CheckEquipment(equipmentManufacturerModel.Manufacturer, equipmentManufacturerModel.Model);
                if (!isEquipmentExists)
                {
                    bool result = _equipmentManufacturerService.Create(equipmentManufacturerModel);
                    return result ? Ok() : NotFound();
                }
                return BadRequest("Такое оборудование уже существует");
            }
            return BadRequest();
        }


        [HttpPatch("{id}")]
        public IActionResult UpdateEquipmentManufacturer(int id, [FromBody] EquipmentManufacturerModel equipmentManufacturerModel)
        {
            if (equipmentManufacturerModel != null)
            {
                bool result = _equipmentManufacturerService.Update(id, equipmentManufacturerModel);
                return result ? Ok() : NotFound();
            }
            return BadRequest();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteEquipmentManufacturer(int id)
        {
            bool result = _equipmentManufacturerService.Delete(id);
            return result ? Ok() : NotFound();
        }
    }
}
