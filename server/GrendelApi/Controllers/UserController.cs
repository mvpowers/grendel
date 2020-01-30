using GrendelApi.Services;
using GrendelData.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GrendelApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        [AllowAnonymous]
        [HttpPost("auth")]
        public IActionResult Authenticate([FromBody]UserAuthRequest userAuthRequest)
        {
            var user = _userService.Authenticate(userAuthRequest.Phone, userAuthRequest.Password);

            if (user == null)
                return BadRequest(new { message = "Phone or password is incorrect" });

            return Ok(user);
        }
        
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
    }
}