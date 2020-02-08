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

            if (user == null) return NotFound();

            return Ok(user.ToUserView());
        }

        [AllowAnonymous]
        [HttpPost("reset-token")]
        public async Task<ActionResult> CreateUserResetToken([FromBody] UserTokenRequest userTokenRequest)
        {
            var user = await _userService.CreateUserPasswordResetToken(userTokenRequest.Phone);
            
            if (user == null) return NotFound();

            return Ok(user.ToUserView());
        }
        
        [AllowAnonymous]
        [HttpPost("password-reset")]
        public async Task<ActionResult<UserView>> ResetUserPassword([FromBody] UserPasswordResetRequest resetRequest)
        {
            if (resetRequest.NewPassword != resetRequest.NewPasswordConfirm)
                return BadRequest(new { message = "New password and confirmation does not match" });
            
            var user = await _userService.UpdateUserPassword(resetRequest.UserResetToken, resetRequest.NewPassword);
            
            if (user == null) return NotFound();

            return Ok(user.ToUserView());
        }

        [AllowAnonymous]
        [HttpGet(("reset-token/{token}"))]
        public async Task<ActionResult<UserView>> GetUserFromResetToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                return BadRequest(new {message = "Token cannot be empty"});

            var user = await _userService.GetUserFromResetToken(token);

            if (user == null) return NotFound();

            return Ok(user.ToUserView());
        }
    }
}