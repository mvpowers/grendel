using System.Threading.Tasks;
using GrendelData;
using GrendelData.Questions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GrendelApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionController : ControllerBase
    {
        private readonly ILogger<QuestionController> _logger;
        private readonly GrendelContext _context;

        public QuestionController(ILogger<QuestionController> logger, GrendelContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("{questionId}")]
        public async Task<ActionResult<Question>> ReadQuestionById(int questionId)
        {
            var question = await _context
                .Questions
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == questionId);

            if (question == null)
            {
                return NotFound();
            }
            
            return Ok(question);
        }
        
        [HttpGet]
        public async Task<ActionResult<Question>> ReadActiveQuestion()
        {
            var question = await _context
                .Questions
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IsActive == true);

            if (question == null)
            {
                return NotFound();
            }
            
            return Ok(question);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateQuestion([FromBody]Question question)
        {
            await _context.Questions.AddAsync(question);
            await _context.SaveChangesAsync();

            return Ok(question.Id);
        }
    }
}
