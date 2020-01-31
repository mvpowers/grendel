using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrendelApi.Services;
using GrendelData;
using GrendelData.Votes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GrendelApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route( "api/v{version:apiVersion}/[controller]" )]
    public class VoteController : ControllerBase
    {
        private readonly ILogger<VoteController> _logger;
        private readonly GrendelContext _context;
        private readonly IUserService _userService;

        public VoteController(ILogger<VoteController> logger, GrendelContext context, IUserService userService)
        {
            _logger = logger;
            _context = context;
            _userService = userService;
        }

        [HttpGet("active")]
        public async Task<ActionResult<List<VoteView>>> ReadActiveVotes()
        {
            var activeQuestionId = await _context
                .Questions
                .AsNoTracking()
                .Where(x => x.IsActive == true)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();
            
            var votes = await _context
                .Votes
                .AsNoTracking()
                .Where(x => x.QuestionId == activeQuestionId)
                .ToListAsync();

            if (votes == null) return NotFound();
            
            return Ok(votes.ToVoteView());
        }

        [HttpPost]
        public async Task<ActionResult<VoteView>> CreateVote([FromBody]VoteCreateRequest request)
        {
            try
            {
                var authHeader = Request.Headers["Authorization"];
                var userId = await _userService.GetUserIdFromAuthHeader(authHeader); 
                var vote = request.ToVote(userId);
                
                await _context.Votes.AddAsync(vote);
                await _context.SaveChangesAsync();
                return Ok(vote.ToVoteView());
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}