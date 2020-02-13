using System;
using System.Threading.Tasks;
using GrendelData;
using GrendelData.Questions;
using GrendelData.Users;
using Microsoft.Extensions.Options;

namespace GrendelApi.Services
{
    public interface IQuestionService
    {
        Task<Question> CreateQuestion(string authHeader, QuestionCreateRequest questionCreateRequest);
        Task<Question> ReadActiveQuestion();
        Task<Question> SetNewActiveQuestion();
        Task<Question> ExpireActiveQuestion();
    }
    
    public class QuestionService : IQuestionService
    {
        private readonly IAppSettings _appSettings;
        private readonly IQuestionRepository _questionRepository;
        private readonly IUserRepository _userRepository;

        public QuestionService(IOptions<AppSettings> appSettings, IQuestionRepository questionRepository, IUserRepository userRepository)
        {
            _appSettings = appSettings.Value;
            _questionRepository = questionRepository;
            _userRepository = userRepository;
        }

        public async Task<Question> ReadActiveQuestion()
        {
            return await _questionRepository.ReadActiveQuestion();
        }

        public async Task<Question> CreateQuestion(string authHeader, QuestionCreateRequest questionCreateRequest)
        {
            var user = await _userRepository.GetUserFromAuthHeader(authHeader);
            var question = await _questionRepository.CreateQuestion(user, questionCreateRequest);

            return question;
        }

        public async Task<Question> SetNewActiveQuestion()
        {
            var currentActiveQuestion = await _questionRepository.ReadActiveQuestion();
            if (currentActiveQuestion != null)
            {
                currentActiveQuestion.IsActive = false;
                await _questionRepository.UpdateQuestion(currentActiveQuestion);
            }

            var nextActiveQuestion = await _questionRepository.ReadNextActiveQuestion();
            nextActiveQuestion.TimeAsked = DateTime.Now;
            nextActiveQuestion.IsActive = true;
            await _questionRepository.UpdateQuestion(nextActiveQuestion);

            return nextActiveQuestion;
        }

        public async Task<Question> ExpireActiveQuestion()
        {
            var currentActiveQuestion = await _questionRepository.ReadActiveQuestion();
            if (currentActiveQuestion == null) throw new ArgumentNullException(nameof(currentActiveQuestion));
            
            var isSessionActive = DateTime.Now < currentActiveQuestion.TimeAsked?.AddMinutes(_appSettings.VoteSessionDurationMinutes);
            if (isSessionActive)
            {
                currentActiveQuestion.TimeAsked = DateTime.Today;
                await _questionRepository.UpdateQuestion(currentActiveQuestion);
            }

            return currentActiveQuestion;
        }
    }
}