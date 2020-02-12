using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GrendelApi.Exceptions;
using GrendelApi.Services;
using GrendelData.Questions;
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
        private readonly IQuestionService _questionService;
        private readonly IVoteService _voteService;
        private readonly ILikeService _likeService;

        public LikeController(ILogger<LikeController> logger, IQuestionService questionService, IVoteService voteService, ILikeService likeService)
        {
            _logger = logger;
            _questionService = questionService;
            _voteService = voteService;
            _likeService = likeService;
        }

        
        [HttpGet("active/self")]
        public async Task<ActionResult<List<int>>> ReadActiveSelfLikes()
        {
            try
            {
                var question = await _questionService.ReadActiveQuestion();
                if (question == null) throw new ReadEntityException(typeof(Question));
            
                var voteSessionDurationMinutes = _voteService.ReadVoteSessionDurationMinutes();
                var questionView = question.ToQuestionView(voteSessionDurationMinutes);
            
                return Ok(questionView);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return UnprocessableEntity(new ErrorResponse(e.Message));
            }
        }
    }
}
