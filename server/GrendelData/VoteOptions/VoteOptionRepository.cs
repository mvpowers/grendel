using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrendelData.Votes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GrendelData.VoteOptions
{
    public interface IVoteOptionRepository
    {
        Task<VoteOption> CreateVoteOption(VoteOptionCreateRequest voteOptionCreateRequest);
        Task<List<VoteOption>> ReadActiveVoteOptions();
    }
    
    public class VoteOptionRepository : IVoteOptionRepository
    {
        private readonly ILogger<Vote> _logger;
        private readonly GrendelContext _context;

        public VoteOptionRepository(ILogger<Vote> logger, GrendelContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<List<VoteOption>> ReadActiveVoteOptions()
        {
            try
            {
                var voteOptions = await _context
                    .VoteOptions
                    .AsNoTracking()
                    .Where(x => x.IsActive == true)
                    .ToListAsync();

                return voteOptions;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return null;
        }

        public async Task<VoteOption> CreateVoteOption(VoteOptionCreateRequest voteOptionCreateRequest)
        {
            if (voteOptionCreateRequest == null) throw new ArgumentNullException(nameof(voteOptionCreateRequest));

            try
            {
                var voteOption = voteOptionCreateRequest.ToVoteOption();
                await _context.VoteOptions.AddAsync(voteOption);
                await _context.SaveChangesAsync();

                return voteOption;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return null;
        }
    }
}