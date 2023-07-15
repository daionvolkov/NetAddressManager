using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetAddressManager.Api.Models.Services;
using NetAddressManager.Api.Models;
using Microsoft.AspNetCore.Authorization;
using NetAddressManager.Models;
using Microsoft.EntityFrameworkCore;

namespace NetAddressManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private readonly UserService _userService;
        public UserController(ApplicationContext db)
        {
            _db = db;
            _userService = new UserService(db);
        }

        [HttpGet]
        
        public async Task<IEnumerable<UserModel>> GetUsers()
        {
            return await _db.User.Select(u => u.GetModel()).ToListAsync();
        }
        
        [HttpGet("{id}")]
        public ActionResult<UserModel> GetUserById(int id)
        {
            var user = _userService.Get(id);
            return user == null ? NotFound() : Ok(user);
        }



        [HttpPost]
        public IActionResult CreateUser([FromBody] UserModel userModel)
        {
            if (userModel != null)
            {
                bool result = _userService.Create(userModel);
                return result ? Ok() : NotFound();
            }
            return BadRequest();
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserModel userModel)
        {
            if (userModel != null)
            {
                bool result = _userService.Update(id, userModel);
                return result ? Ok() : NotFound();
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            bool result = _userService.Delete(id);
            return result ? Ok() : NotFound();
        }

    }
}
