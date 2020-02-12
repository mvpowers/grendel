using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GrendelData;
using GrendelData.Questions;
using GrendelData.Users;
using GrendelData.Votes;
using Microsoft.Extensions.Options;

namespace GrendelApi.Services
{
    public interface IVoteService
    {
        Task<Vote> CreateVote(string authHeader, VoteCreateRequest voteCreateRequest);
        Task<List<VoteView>> ReadActiveVotes(int userId);
        int ReadVoteSessionDurationMinutes();
        Task<bool> UserHasActiveVote(int userId);
        Task<bool> HasVotingExpired();
    }
    
    public class VoteService : IVoteService
    {
        private readonly IUserRepository _userRepository;
        private readonly IVoteRepository _voteRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IOptions<AppSettings> _appSettings;

        public VoteService(IUserRepository userRepository, IVoteRepository voteRepository, IQuestionRepository questionRepository, IOptions<AppSettings> appSettings)
        {
            _userRepository = userRepository;
            _voteRepository = voteRepository;
            _questionRepository = questionRepository;
            _appSettings = appSettings;
        }

        public async Task<List<VoteView>> ReadActiveVotes(int userId)
        {
            var votes = await _voteRepository.ReadActiveVotes();
            
            return votes.ToVoteView(userId);
        }

        public async Task<Vote> CreateVote(string authHeader, VoteCreateRequest voteCreateRequest)
        {
            var user = await _userRepository.GetUserFromAuthHeader(authHeader);
            var vote = await _voteRepository.CreateVote(user, voteCreateRequest);

            return vote;
        }

        public int ReadVoteSessionDurationMinutes()
        {
            var appSettings = _appSettings.Value;
            return appSettings.VoteSessionDurationMinutes;
        }
        
        public async Task<bool> UserHasActiveVote(int userId)
        {
            var activeQuestion = await _questionRepository.ReadActiveQuestion();
            var activeUserVote = await _voteRepository.GetVoteByUserId(userId, activeQuestion.Id);

            return activeUserVote != null;
        }

        public async Task<bool> HasVotingExpired()
        {
            var voteSessionDurationMinutes = ReadVoteSessionDurationMinutes();
            var activeQuestion = await _questionRepository.ReadActiveQuestion();
            var questionTimeExpires = activeQuestion.TimeAsked?.AddMinutes(voteSessionDurationMinutes);

            return DateTime.Now > questionTimeExpires;
        }
    }
}