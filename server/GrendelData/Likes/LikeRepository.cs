using System;
using System.Threading.Tasks;
using GrendelData.Questions;
using Microsoft.Extensions.Logging;

namespace GrendelData.Likes
{
    public interface ILikeRepository
    {
        Task<Like> CreateLike(int userId, int voteId);
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
    }
}