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
    public class QuestionController : ControllerBase
    {
        private readonly ILogger<QuestionController> _logger;
        private readonly IQuestionService _questionService;
        private readonly IVoteService _voteService;

        public QuestionController(ILogger<QuestionController> logger, IQuestionService questionService, IVoteService voteService)
        {
            _logger = logger;
            _questionService = questionService;
            _voteService = voteService;
        }

        
        [HttpGet("active")]
        public async Task<ActionResult<QuestionView>> ReadActiveQuestion()
        {
            try
            {
                var question = await _questionService.ReadActiveQuestion();
                if (question == null) throw new ReadEntityException(typeof(Question));
            
                return Ok(question.ToQuestionView());
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return UnprocessableEntity(new ErrorResponse(e.Message));
            }
        }

        [HttpGet("queue")]
        public async Task<ActionResult<List<QuestionQueueView>>> ReadQueuedQuestions()
        {
            var questions = await _questionService.ReadQueuedQuestions();
            if (questions == null) return new List<QuestionQueueView>();

            return questions.ToQuestionQueueView();
        }

        [HttpPost]
        public async Task<ActionResult> CreateQuestion([FromBody]QuestionCreateRequest request)
        {
            try
            {
                var authHeader = Request.Headers["Authorization"];
                
                var question = await _questionService.CreateQuestion(authHeader, request);
                if (question == null) throw new CreateEntityException(typeof(Question));
            
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return UnprocessableEntity(new ErrorResponse(e.Message));
            }
        }

        [HttpDelete("{questionId}")]
        public async Task<ActionResult> DeleteQuestion(int questionId)
        {
            try
            {
                await _questionService.DeleteQuestion(questionId);
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
