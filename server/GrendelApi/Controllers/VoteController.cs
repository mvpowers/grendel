using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public VoteController(ILogger<VoteController> logger, IVoteService voteService)
        {
            _logger = logger;
            _voteService = voteService;
        }

        [HttpGet("active")]
        public async Task<ActionResult<List<VoteView>>> ReadActiveVotes()
        {
            var votes = await _voteService.ReadActiveVotes();

            if (votes == null) return NotFound();
            
            return Ok(votes.ToVoteView());
        }

        [HttpPost]
        public async Task<ActionResult<VoteView>> CreateVote([FromBody]VoteCreateRequest request)
        {
            try
            {
                var authHeader = Request.Headers["Authorization"];
                var vote = await _voteService.CreateVote(authHeader, request);
                
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