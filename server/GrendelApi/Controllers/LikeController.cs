using System;
using System.Threading.Tasks;
using GrendelApi.Exceptions;
using GrendelApi.Services;
using GrendelData.Likes;
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
    public class LikeController : ControllerBase
    {
        private readonly ILogger<LikeController> _logger;
        private readonly IUserService _userService;
        private readonly IVoteService _voteService;
        private readonly ILikeService _likeService;

        public LikeController(ILogger<LikeController> logger, IUserService userService, IVoteService voteService, ILikeService likeService)
        {
            _logger = logger;
            _userService = userService;
            _voteService = voteService;
            _likeService = likeService;
        }

        
        [HttpPost]
        public async Task<ActionResult<VoteView>> CreateLike([FromBody] LikeCreateRequest likeCreateRequest)
        {
            try
            {
                var authHeader = Request.Headers["Authorization"];
                var user = await _userService.GetUserFromAuthHeader(authHeader);
                if (user == null) throw new UserNotFoundException();

                var like = await _likeService.CreateLike(user.Id, likeCreateRequest.VoteId);
                if (like == null) throw new ArgumentNullException(nameof(like));

                var vote = await _voteService.ReadVoteById(likeCreateRequest.VoteId);
                if (vote == null) throw new ArgumentNullException(nameof(vote));

                return Ok(vote.ToVoteView(user.Id));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return UnprocessableEntity(new ErrorResponse(e.Message));
            }
        }
    }
}
