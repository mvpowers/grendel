using System.Threading.Tasks;
using GrendelApi.Services;
using GrendelData;
using GrendelData.Questions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GrendelApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route( "api/v{version:apiVersion}/[controller]" )]
    public class QuestionController : ControllerBase
    {
        private readonly ILogger<QuestionController> _logger;
        private readonly GrendelContext _context;
        private readonly IUserService _userService;

        public QuestionController(ILogger<QuestionController> logger, GrendelContext context, IUserService userService)
        {
            _logger = logger;
            _context = context;
            _userService = userService;
        }

        [HttpGet("{questionId}")]
        public async Task<ActionResult<QuestionView>> ReadQuestionById(int questionId)
        {
            var question = await _context
                .Questions
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == questionId);

            if (question == null) return NotFound();
            
            return Ok(question.ToQuestionView());
        }
        
        [HttpGet("active")]
        public async Task<ActionResult<QuestionView>> ReadActiveQuestion()
        {
            var question = await _context
                .Questions
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IsActive);

            if (question == null) return NotFound();
            
            return Ok(question.ToQuestionView());
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateQuestion([FromBody]QuestionCreateRequest request)
        {
            var authHeader = Request.Headers["Authorization"];
            var userId = await _userService.GetUserIdFromAuthHeader(authHeader);
            var question = request.ToQuestion(userId);
            
            await _context.Questions.AddAsync(question);
            await _context.SaveChangesAsync();

            return Ok(question.Id);
        }
    }
}
