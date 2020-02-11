using System;
using System.Threading.Tasks;
using GrendelApi.Exceptions;
using GrendelApi.Services;
using GrendelData.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GrendelApi.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route( "api/v{version:apiVersion}/[controller]" )]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        private readonly IVoteService _voteService;

        public UserController(ILogger<UserController> logger, IUserService userService, IVoteService voteService)
        {
            _logger = logger;
            _userService = userService;
            _voteService = voteService;
        }
        
        [AllowAnonymous]
        [HttpPost("auth")]
        public async Task<ActionResult<UserView>> AuthenticateUser([FromBody]UserAuthRequest userAuthRequest)
        {
            try
            {
                var user = await _userService.Authenticate(userAuthRequest.Phone, userAuthRequest.Password);
                if (user == null) throw new UserNotFoundException();

                return Ok(user.ToUserView());
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return UnprocessableEntity(new ErrorResponse(e.Message));
            }
        }

        [AllowAnonymous]
        [HttpPost("reset-token")]
        public async Task<ActionResult> CreateUserResetToken([FromBody] UserTokenRequest userTokenRequest)
        {
            try
            {
                var user = await _userService.CreateUserPasswordResetToken(userTokenRequest.Phone);
                if (user == null) throw new UserNotFoundException();

                return Ok(user.ToUserView());
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return UnprocessableEntity(new ErrorResponse(e.Message));
            }
        }
        
        [AllowAnonymous]
        [HttpPost("password-reset")]
        public async Task<ActionResult<UserView>> ResetUserPassword([FromBody] UserPasswordResetRequest resetRequest)
        {
            if (resetRequest.NewPassword != resetRequest.NewPasswordConfirm)
                return BadRequest(new ErrorResponse("New password and confirmation does not match"));

            try
            {
                var user = await _userService.UpdateUserPassword(resetRequest.UserResetToken, resetRequest.NewPassword);
                if (user == null) throw new UserNotFoundException();

                return Ok(user.ToUserView());
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return UnprocessableEntity(new ErrorResponse(e.Message));
            }
            
        }

        [AllowAnonymous]
        [HttpGet(("reset-token/{token}"))]
        public async Task<ActionResult<UserView>> GetUserFromResetToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                return BadRequest(new ErrorResponse("Token cannot be empty"));

            try
            {
                var user = await _userService.GetUserFromResetToken(token);
                if (user == null) throw new UserNotFoundException();

                return Ok(user.ToUserView());
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return UnprocessableEntity(new ErrorResponse(e.Message));
            }
        }
    }
}