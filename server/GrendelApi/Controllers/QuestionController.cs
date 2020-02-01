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

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        
        [HttpGet("active")]
        public async Task<ActionResult<QuestionView>> ReadActiveQuestion()
        {
            var question = await _questionService.ReadActiveQuestion();

            return Ok(question.ToQuestionView());
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
