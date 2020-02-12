using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GrendelApi.Exceptions;
using GrendelApi.Services;
using GrendelData.Votes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GrendelApi.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route( "api/v{version:apiVersion}/[controller]" )]
    public class VoteController : ControllerBase
    {
        private readonly ILogger<VoteController> _logger;
        private readonly IVoteService _voteService;
        private readonly IUserService _userService;

        public VoteController(ILogger<VoteController> logger, IVoteService voteService, IUserService userService)
        {
            _logger = logger;
            _voteService = voteService;
            _userService = userService;
        }

        [HttpGet("active")]
        public async Task<ActionResult<List<VoteView>>> ReadActiveVotes()
        {
            try
            {
                var authHeader = Request.Headers["Authorization"];
                var user = await _userService.GetUserFromAuthHeader(authHeader);
                
                var voteViews = await _voteService.ReadActiveVotes(user.Id);
                if (voteViews == null) throw new ReadEntityException(typeof(Vote));
            
                return Ok(voteViews);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return UnprocessableEntity(new ErrorResponse(e.Message));
            }
        }

        [HttpPost]
        public async Task<ActionResult<VoteView>> CreateVote([FromBody]VoteCreateRequest request)
        {
            try
            {
                var authHeader = Request.Headers["Authorization"];
                
                var vote = await _voteService.CreateVote(authHeader, request);
                if (vote == null) throw new CreateEntityException(typeof(Vote));
                
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