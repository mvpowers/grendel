using System.Threading.Tasks;
using GrendelApi.Services;
using GrendelData.Questions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GrendelApi.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route( "api/v{version:apiVersion}/[controller]" )]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        private readonly IVoteService _voteService;

        public QuestionController(IQuestionService questionService, IVoteService voteService)
        {
            _questionService = questionService;
            _voteService = voteService;
        }

        
        [HttpGet("active")]
        public async Task<ActionResult<QuestionView>> ReadActiveQuestion()
        {
            var question = await _questionService.ReadActiveQuestion();
            var voteSessionDurationMinutes = _voteService.ReadVoteSessionDurationMinutes();
            var questionView = question.ToQuestionView(voteSessionDurationMinutes);

            return Ok(questionView);
        }

        [HttpPost]
        public async Task<ActionResult> CreateQuestion([FromBody]QuestionCreateRequest request)
        {
            var authHeader = Request.Headers["Authorization"];
            var question = await _questionService.CreateQuestion(authHeader, request);
            
            return Ok(question.Id);
        }
    }
}
