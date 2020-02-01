using System;
using System.Threading.Tasks;
using GrendelData.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GrendelData.Questions
{
    public interface IQuestionRepository
    {
        Task<Question> ReadActiveQuestion();
        Task<Question> CreateQuestion(User user, QuestionCreateRequest questionCreateRequest);
    }
    
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ILogger<Question> _logger;
        private readonly GrendelContext _context;

        public QuestionRepository(ILogger<Question> logger, GrendelContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Question> ReadActiveQuestion()
        {
            try
            {
                var question = await _context
                    .Questions
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.IsActive);

                return question;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return null;
        }

        public async Task<Question> CreateQuestion(User user, QuestionCreateRequest questionCreateRequest)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (questionCreateRequest == null) throw new ArgumentNullException(nameof(questionCreateRequest));
            
            try
            {
                var question = questionCreateRequest.ToQuestion(user.Id);
                await _context.Questions.AddAsync(question);
                await _context.SaveChangesAsync();

                return question;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return null;
        }
    }
}