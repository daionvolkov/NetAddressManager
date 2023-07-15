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
    public class PostalAddressController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private readonly PostalAddressService _postalAddressService;
        private readonly CheckDataService _checkDataService;

        public PostalAddressController(ApplicationContext db)
        {
            _db = db;
            _postalAddressService = new PostalAddressService(db);
            _checkDataService = new CheckDataService(db);
        }


        [HttpGet]
        public async Task<IEnumerable<PostalAddressModel>> GetPostalAddresses()
        {
            return await _db.PostalAddress.Select(pa => pa.GetModel()).ToListAsync();
        }


        [HttpGet("{id}")]
        public ActionResult<PostalAddressModel> GetPostalAddressById(int id)
        {
            var address = _postalAddressService.Get(id);
            return address == null ? NotFound() : Ok(address);
        }


        [HttpPost]
        public IActionResult CreatePostalAddress([FromBody] PostalAddressModel postalAddressModel)
        {
            if (postalAddressModel != null)
            {
                bool isPostalAdressExists = _checkDataService.CheckPostalAddress(postalAddressModel.City, postalAddressModel.Street, postalAddressModel.Building);
                if (!isPostalAdressExists)
                {
                    bool result = _postalAddressService.Create(postalAddressModel);
                    return result ? Ok() : NotFound();
                }
                return BadRequest("Такой почтовый адрес уже существует");
            }
            return BadRequest();
        }

        [HttpPatch("{id}")]
        public IActionResult UpdatePostalAddress(int id, [FromBody] PostalAddressModel postalAddressModel)
        {
            if (postalAddressModel != null)
            {
                bool result = _postalAddressService.Update(id, postalAddressModel);
                return result ? Ok() : NotFound();
            }
            return BadRequest();
        }


        [HttpDelete("{id}")]
        public IActionResult DeletePostalAddreess(int id)
        {
            bool result = _postalAddressService.Delete(id);
            return result ? Ok() : NotFound();
        }
    }
}
