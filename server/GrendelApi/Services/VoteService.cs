using System.Collections.Generic;
using System.Threading.Tasks;
using GrendelData.Questions;
using GrendelData.Users;
using GrendelData.Votes;

namespace GrendelApi.Services
{
    public interface IVoteService
    {
        Task<Vote> CreateVote(string authHeader, VoteCreateRequest voteCreateRequest);
        Task<List<VoteView>> ReadActiveVotes(int userId);
        Task<List<VoteView>> ReadVotesByQuestionId(int questionId);
        Task<bool> UserHasActiveVote(int userId);
        Task<bool> HasActiveSession();
        Task<Vote> ReadVoteById(int voteId);
    }
    
    public class VoteService : IVoteService
    {
        private readonly IUserRepository _userRepository;
        private readonly IVoteRepository _voteRepository;
        private readonly IQuestionRepository _questionRepository;

        public VoteService(IUserRepository userRepository, IVoteRepository voteRepository, IQuestionRepository questionRepository)
        {
            _userRepository = userRepository;
            _voteRepository = voteRepository;
            _questionRepository = questionRepository;
        }

        public async Task<List<VoteView>> ReadActiveVotes(int userId)
        {
            var votes = await _voteRepository.ReadActiveVotes();
            
            return votes.ToVoteView(userId);
        }
        
        public async Task<List<VoteView>> ReadVotesByQuestionId(int questionId)
        {
            var votes = await _voteRepository.ReadVotesByQuestionId(questionId);
            
            return votes.ToVoteView();
        }

        public async Task<Vote> CreateVote(string authHeader, VoteCreateRequest voteCreateRequest)
        {
            var user = await _userRepository.GetUserFromAuthHeader(authHeader);
            var vote = await _voteRepository.CreateVote(user, voteCreateRequest);

            return vote;
        }

        public async Task<bool> UserHasActiveVote(int userId)
        {
            var activeQuestion = await _questionRepository.ReadActiveQuestion();
            var activeUserVote = await _voteRepository.GetVoteByUserId(userId, activeQuestion.Id);

            return activeUserVote != null;
        }

        public async Task<bool> HasActiveSession()
        {
            var activeQuestion = await _questionRepository.ReadActiveQuestion();

            return activeQuestion.IsSessionActive == true;
        }

        public async Task<Vote> ReadVoteById(int voteId)
        {
            var vote = await _voteRepository.GetVoteByVoteId(voteId);
            return vote;
        }
    }
}