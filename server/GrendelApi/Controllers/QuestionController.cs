// using System;
// using System.Collections.Generic;
// using System.Linq;
using System.Threading.Tasks;
using GrendelData;
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
        public async Task<ActionResult<Question>> ReadQuestion(int questionId)
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

        [HttpPost]
        public async Task<ActionResult<int>> CreateQuestion(Question question)
        {
            await _context.Questions.AddAsync(question);
            await _context.SaveChangesAsync();

            return Ok(question.Id);
        }
    }
}
