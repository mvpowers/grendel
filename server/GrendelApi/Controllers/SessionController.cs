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
    public class SessionController : ControllerBase
    {
        private readonly ILogger<SessionController> _logger;
        private readonly IUserService _userService;
        private readonly IQuestionService _questionService;
        private readonly IVoteService _voteService;

        public SessionController(ILogger<SessionController> logger, IUserService userService, IQuestionService questionService, IVoteService voteService)
        {
            _logger = logger;
            _userService = userService;
            _questionService = questionService;
            _voteService = voteService;
        }

        
        [HttpGet("user-info")]
        public async Task<ActionResult<UserSessionView>> GetUserSessionInfo()
        {
            try
            {
                var authHeader = Request.Headers["Authorization"];
                var user = await _userService.GetUserFromAuthHeader(authHeader);
                if (user == null) throw new UserNotFoundException();

                var hasVotingExpired = await _voteService.HasVotingExpired();
                var hasActiveVote = await _voteService.UserHasActiveVote(user.Id);

                return user.ToUserSessionView(hasVotingExpired, hasActiveVote);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return UnprocessableEntity(new ErrorResponse(e.Message));
            }
        }
        
        [HttpGet("start")]
        public async Task<ActionResult> StartSession()
        {
            try
            {
                var authHeader = Request.Headers["Authorization"];
                var user = await _userService.GetUserFromAuthHeader(authHeader);
                if (user.IsAdmin != true) return Forbid();

                await _questionService.SetNewActiveQuestion();
                
                // todo add text notification

                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return UnprocessableEntity(new ErrorResponse(e.Message));
            }
        }
        
        [HttpGet("expire")]
        public async Task<ActionResult> EndSession()
        {
            try
            {
                var authHeader = Request.Headers["Authorization"];
                var user = await _userService.GetUserFromAuthHeader(authHeader);
                if (user.IsAdmin != true) return Forbid();

                await _questionService.ExpireActiveQuestion();

                // todo add text notification

                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return UnprocessableEntity(new ErrorResponse(e.Message));
            }
        }
    }
}
