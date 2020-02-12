using System;
using System.Threading.Tasks;
using GrendelData.Questions;
using GrendelData.Users;

namespace GrendelApi.Services
{
    public interface IQuestionService
    {
        Task<Question> CreateQuestion(string authHeader, QuestionCreateRequest questionCreateRequest);
        Task<Question> ReadActiveQuestion();
        Task<Question> SetNewActiveQuestion();
    }
    
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IUserRepository _userRepository;

        public QuestionService(IQuestionRepository questionRepository, IUserRepository userRepository)
        {
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
            currentActiveQuestion.IsActive = false;
            await _questionRepository.UpdateQuestion(currentActiveQuestion);

            var nextActiveQuestion = await _questionRepository.ReadNextActiveQuestion();
            nextActiveQuestion.TimeAsked = DateTime.Now;
            nextActiveQuestion.IsActive = true;
            await _questionRepository.UpdateQuestion(nextActiveQuestion);

            return nextActiveQuestion;
        }
    }
}