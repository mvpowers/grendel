using System.Collections.Generic;
using System.Threading.Tasks;
using GrendelData.VoteOptions;

namespace GrendelApi.Services
{
    public interface IVoteOptionService
    {
        Task<VoteOption> CreateVoteOption(VoteOptionCreateRequest voteOptionCreateRequest);
        Task<List<VoteOption>> ReadVoteOptions();
        Task<List<VoteOption>> ReadActiveVoteOptions();
    }
    
    public class VoteOptionService : IVoteOptionService
    {
        private readonly IVoteOptionRepository _voteOptionRepository;

        public VoteOptionService(IVoteOptionRepository voteOptionRepository)
        {
            _voteOptionRepository = voteOptionRepository;
        }

        public async Task<List<VoteOption>> ReadVoteOptions()
        {
            return await _voteOptionRepository.ReadVoteOptions();
        }
        
        public async Task<List<VoteOption>> ReadActiveVoteOptions()
        {
            return await _voteOptionRepository.ReadActiveVoteOptions();
        }

        public async Task<VoteOption> CreateVoteOption(VoteOptionCreateRequest voteOptionCreateRequest)
        {
            return await _voteOptionRepository.CreateVoteOption(voteOptionCreateRequest);
        }
    }
}