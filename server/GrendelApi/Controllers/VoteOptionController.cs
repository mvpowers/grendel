using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrendelData;
using GrendelData.VoteOptions;
using GrendelData.Votes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GrendelApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VoteOptionController : ControllerBase
    {
        private readonly ILogger<VoteOptionController> _logger;
        private readonly GrendelContext _context;

        public VoteOptionController(ILogger<VoteOptionController> logger, GrendelContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<VoteOptionView>>> ReadActiveVoteOptions()
        {
            var voteOptions = await _context
                .VoteOptions
                .AsNoTracking()
                .Where(x => x.IsActive == true)
                .ToListAsync();

            if (voteOptions == null) return NotFound();
            
            return Ok(voteOptions.ToVoteOptionView());
        }

        [HttpPost]
        public async Task<ActionResult<VoteOptionView>> CreateVoteOption([FromBody]VoteOptionCreateRequest request)
        {
            try
            {
                var voteOption = request.ToVoteOption();
                await _context.VoteOptions.AddAsync(voteOption);
                await _context.SaveChangesAsync();
                return Ok(voteOption.ToVoteOptionView());
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}