using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrendelData.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GrendelData.Votes
{
    public interface IVoteRepository
    {
        Task<Vote> CreateVote(User user, VoteCreateRequest voteCreateRequest);
        Task<List<Vote>> ReadActiveVotes();
        Task<Vote> GetVoteByUserId(int userId, int voteId);
        Task<Vote> GetVoteByVoteId(int voteId);
    }
    
    public class VoteRepository : IVoteRepository
    {
        private readonly ILogger<Vote> _logger;
        private readonly GrendelContext _context;

        public VoteRepository(ILogger<Vote> logger, GrendelContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<List<Vote>> ReadActiveVotes()
        {
            try
            {
                var activeQuestionId = await _context
                    .Questions
                    .AsNoTracking()
                    .Where(x => x.IsQuestionActive == true)
                    .Select(x => x.Id)
                    .FirstOrDefaultAsync();
            
                var votes = await _context
                    .Votes
                    .AsNoTracking()
                    .Where(x => x.QuestionId == activeQuestionId)
                    .Include(x => x.Likes)
                    .ToListAsync();

                return votes;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return null;
        }

        public async Task<Vote> CreateVote(User user, VoteCreateRequest voteCreateRequest)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (voteCreateRequest == null) throw new ArgumentNullException(nameof(voteCreateRequest));

            try
            {
                var vote = voteCreateRequest.ToVote(user.Id);
                await _context.Votes.AddAsync(vote);
                await _context.SaveChangesAsync();

                return vote;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return null;
        }

        public async Task<Vote> GetVoteByUserId(int userId, int questionId)
        {
            if (userId <= 0) throw new ArgumentOutOfRangeException(nameof(userId));
            if (questionId <= 0) throw new ArgumentOutOfRangeException(nameof(questionId));
            
            try
            {
                var vote = await _context.Votes
                    .Where(x => x.UserId == userId)
                    .Where(x => x.QuestionId == questionId)
                    .FirstOrDefaultAsync();

                return vote;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return null;
        }
        
        public async Task<Vote> GetVoteByVoteId(int voteId)
        {
            if (voteId <= 0) throw new ArgumentOutOfRangeException(nameof(voteId));

            try
            {
                var vote = await _context.Votes
                    .Where(x => x.Id == voteId)
                    .Include(x => x.Likes)
                    .FirstOrDefaultAsync();

                return vote;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return null;
        }
    }
}