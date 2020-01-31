using System.Threading.Tasks;
using GrendelApi.Services;
using GrendelData.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GrendelApi.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route( "api/v{version:apiVersion}/[controller]" )]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        [AllowAnonymous]
        [HttpPost("auth")]
        public async Task<ActionResult<UserView>> AuthenticateUser([FromBody]UserAuthRequest userAuthRequest)
        {
            var user = await _userService.Authenticate(userAuthRequest.Phone, userAuthRequest.Password);

            if (user == null) return BadRequest(new { message = "Phone or password is incorrect" });

            return Ok(user.ToUserView());
        }
    }
}