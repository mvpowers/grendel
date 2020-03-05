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
        private readonly ITextService _textService;

        public SessionController(ILogger<SessionController> logger, IUserService userService, IQuestionService questionService, IVoteService voteService, ITextService textService)
        {
            _logger = logger;
            _userService = userService;
            _questionService = questionService;
            _voteService = voteService;
            _textService = textService;
        }

        
        [HttpGet("user-info")]
        public async Task<ActionResult<UserSessionView>> GetUserSessionInfo()
        {
            try
            {
                var authHeader = Request.Headers["Authorization"];
                var user = await _userService.GetUserFromAuthHeader(authHeader);
                if (user == null) throw new UserNotFoundException();

                var hasActiveSession = await _voteService.HasActiveSession();
                var hasActiveVote = await _voteService.UserHasActiveVote(user.Id);

                return user.ToUserSessionView(hasActiveSession, hasActiveVote);
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
                
                var userPhones = await _userService.GetActiveUserPhones();
                await _textService.SendSessionStartTexts(userPhones);

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
                
                var userPhones = await _userService.GetActiveUserPhones();
                await _textService.SendSessionExpireTexts(userPhones);

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
