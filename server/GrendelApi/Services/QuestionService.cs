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
    }
}