using System.Threading.Tasks;
using GrendelApi.Exceptions;
using GrendelData.Likes;

namespace GrendelApi.Services
{
    public interface ILikeService
    {
        Task<Like> CreateLike(int userId, int voteId);
        Task DeleteLikeByVoteId(int userId, int voteId);
    }
    
    public class LikeService : ILikeService
    {
        private readonly ILikeRepository _likeRepository;

        public LikeService(ILikeRepository likeRepository)
        {
            _likeRepository = likeRepository;
        }

        public async Task<Like> CreateLike(int userId, int voteId)
        {
            var like = await _likeRepository.CreateLike(userId, voteId);
            return like;
        }

        public async Task DeleteLikeByVoteId(int userId, int voteId)
        {
            var like = await _likeRepository.GetLikeByUserIdVoteId(userId, voteId);
            
            await _likeRepository.DeleteLike(like.Id);
        }
    }
}