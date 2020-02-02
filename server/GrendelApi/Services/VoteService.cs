using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GrendelData;
using GrendelData.Users;
using GrendelData.Votes;
using Microsoft.Extensions.Options;

namespace GrendelApi.Services
{
    public interface IVoteService
    {
        Task<Vote> CreateVote(string authHeader, VoteCreateRequest voteCreateRequest);
        Task<List<Vote>> ReadActiveVotes();
        int ReadVoteSessionDurationMinutes();
    }
    
    public class VoteService : IVoteService
    {
        private readonly IUserRepository _userRepository;
        private readonly IVoteRepository _voteRepository;
        private readonly IOptions<AppSettings> _appSettings;

        public VoteService(IUserRepository userRepository, IVoteRepository voteRepository, IOptions<AppSettings> appSettings)
        {
            _userRepository = userRepository;
            _voteRepository = voteRepository;
            _appSettings = appSettings;
        }

        public async Task<List<Vote>> ReadActiveVotes()
        {
            return await _voteRepository.ReadActiveVotes();
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
    }
}