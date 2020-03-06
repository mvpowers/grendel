using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GrendelApi.Exceptions;
using GrendelApi.Services;
using GrendelData.VoteOptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GrendelApi.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route( "api/v{version:apiVersion}/[controller]" )]
    public class VoteOptionController : ControllerBase
    {
        private readonly ILogger<VoteOptionController> _logger;
        private readonly IVoteOptionService _voteOptionService;

        public VoteOptionController(ILogger<VoteOptionController> logger, IVoteOptionService voteOptionService)
        {
            _logger = logger;
            _voteOptionService = voteOptionService;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<VoteOptionView>>> ReadVoteOptions()
        {
            try
            {
                var voteOptions = await _voteOptionService.ReadVoteOptions();
                if (voteOptions == null) throw new ReadEntityException(typeof(VoteOption));
            
                return Ok(voteOptions.ToVoteOptionView());
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return UnprocessableEntity(new ErrorResponse(e.Message));
            }
        }

        [HttpGet("active")]
        public async Task<ActionResult<List<VoteOptionView>>> ReadActiveVoteOptions()
        {
            try
            {
                var voteOptions = await _voteOptionService.ReadActiveVoteOptions();
                if (voteOptions == null) throw new ReadEntityException(typeof(VoteOption));
            
                return Ok(voteOptions.ToVoteOptionView());
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return UnprocessableEntity(new ErrorResponse(e.Message));
            }
        }

        [HttpPost]
        public async Task<ActionResult<VoteOptionView>> CreateVoteOption([FromBody]VoteOptionCreateRequest request)
        {
            try
            {
                var voteOption = await _voteOptionService.CreateVoteOption(request);
                if (voteOption == null) throw new CreateEntityException(typeof(VoteOption));
                
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