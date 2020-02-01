using System.Collections.Generic;
using System.Threading.Tasks;
using GrendelData.Users;
using GrendelData.Votes;

namespace GrendelApi.Services
{
    public interface IVoteService
    {
        Task<Vote> CreateVote(string authHeader, VoteCreateRequest voteCreateRequest);
        Task<List<Vote>> ReadActiveVotes();
    }
    
    public class VoteService : IVoteService
    {
        private readonly IUserRepository _userRepository;
        private readonly IVoteRepository _voteRepository;

        public VoteService(IUserRepository userRepository, IVoteRepository voteRepository)
        {
            _userRepository = userRepository;
            _voteRepository = voteRepository;
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
    }
}