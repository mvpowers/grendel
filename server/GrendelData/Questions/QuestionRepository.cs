using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
        Task<Question> UpdateQuestion(Question question);
        Task<Question> ReadNextActiveQuestion();
        Task<List<Question>> ReadQueuedQuestions();
        Task<Question> ReadQuestionById(int questionId);
        Task DeleteQuestion(Question question);
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
                    .FirstOrDefaultAsync(x => x.IsActive == true);

                return question;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return null;
        }

        public async Task<List<Question>> ReadQueuedQuestions()
        {
            try
            {
                var adminQuestions = await _context
                    .Questions
                    .AsNoTracking()
                    .Where(x => x.TimeAsked == null)
                    .Where(x => x.UserId == 1)
                    .OrderBy(x => x.Id)
                    .Take(3)
                    .ToListAsync();
                
                var userQuestions = await _context
                    .Questions
                    .AsNoTracking()
                    .Where(x => x.TimeAsked == null)
                    .Where(x => x.UserId != 1)
                    .OrderBy(x => x.Id)
                    .Take(3)
                    .ToListAsync();

                var combinedQuestions = adminQuestions.Union(userQuestions).Take(3).ToList();
                
                return combinedQuestions;
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
        
        public async Task<Question> UpdateQuestion(Question question)
        {
            if (question == null) throw new ArgumentNullException(nameof(question));

            try
            {
                _context.Questions.Update(question);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            
            return null;
        }

        public async Task<Question> ReadQuestionById(int questionId)
        {
            try
            {
                var question = await _context.Questions.SingleOrDefaultAsync(x => x.Id == questionId);

                if (question != null) return question;
                throw new NoNullAllowedException("Question not found by ID");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return null;
        }

        public async Task DeleteQuestion(Question question)
        {
            if (question == null) throw new ArgumentNullException(nameof(question));
            
            try
            {
                _context.Questions.Remove(question);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }
        
        public async Task<Question> ReadNextActiveQuestion()
        {
            try
            {
                var newAdminSubmittedQuestion = await _context.Questions
                    .Where(x => x.TimeAsked == null)
                    .Where(x => x.UserId == 1)
                    .OrderBy(x => x.Id)
                    .FirstOrDefaultAsync();

                if (newAdminSubmittedQuestion != null) return newAdminSubmittedQuestion;
                
                var newUserSubmittedQuestion = await _context.Questions
                    .Where(x => x.TimeAsked == null)
                    .OrderBy(x => x.Id)
                    .FirstOrDefaultAsync();
                
                if (newUserSubmittedQuestion != null) return newUserSubmittedQuestion;
                throw new NoNullAllowedException("No unused questions found");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return null;
        }
    }
}