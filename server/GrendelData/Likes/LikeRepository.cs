using System;
using System.Linq;
using System.Threading.Tasks;
using GrendelData.Questions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GrendelData.Likes
{
    public interface ILikeRepository
    {
        Task<Like> CreateLike(int userId, int voteId);
        Task DeleteLike(int likeId);
        Task<Like> GetLikeByUserIdVoteId(int userId, int voteId);
    }
    
    public class LikeRepository : ILikeRepository
    {
        private readonly ILogger<Question> _logger;
        private readonly GrendelContext _context;

        public LikeRepository(ILogger<Question> logger, GrendelContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Like> CreateLike(int userId, int voteId)
        {
            if (userId <= 0) throw new ArgumentOutOfRangeException(nameof(userId));
            if (voteId <= 0) throw new ArgumentOutOfRangeException(nameof(voteId));
            
            try
            {
                var like = new Like()
                {
                    UserId = userId,
                    VoteId = voteId
                };
                
                await _context.AddAsync(like);
                await _context.SaveChangesAsync();

                return like;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return null;
        }

        public async Task DeleteLike(int likeId)
        {
            if (likeId <= 0) throw new ArgumentOutOfRangeException(nameof(likeId));

            try
            {
                var like = await _context.Likes
                    .Where(x => x.Id == likeId)
                    .FirstOrDefaultAsync();

                _context.Remove(like);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }

        public async Task<Like> GetLikeByUserIdVoteId(int userId, int voteId)
        {
            try
            {
                var like = await _context.Likes
                    .Where(x => x.UserId == userId)
                    .Where(x => x.VoteId == voteId)
                    .FirstOrDefaultAsync();

                return like;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return null;
        }
    }
}