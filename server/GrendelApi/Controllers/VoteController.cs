using System;
using System.Linq;
using System.Threading.Tasks;
using GrendelData;
using GrendelData.Votes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GrendelApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VoteController : ControllerBase
    {
        private readonly ILogger<VoteController> _logger;
        private readonly GrendelContext _context;

        public VoteController(ILogger<VoteController> logger, GrendelContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Vote>> ReadCurrentVotes()
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

            if (votes == null)
            {
                return NotFound();
            }
            
            return Ok(votes);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateVote([FromBody]VoteCreateRequest request)
        {
            try
            {
                var vote = request.ToVote();
                await _context.Votes.AddAsync(vote);
                await _context.SaveChangesAsync();
                return Ok(vote.Id);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}