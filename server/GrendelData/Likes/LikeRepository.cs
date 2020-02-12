using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using GrendelData.Likes;
using GrendelData.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GrendelData.Questions
{
    public interface ILikeRepository
    {
        // Task<List<Like>> ReadLikesByQuestionId(int questionId);
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

        // public async Task<List<Like>> ReadLikesByQuestionId(int questionId)
        // {
        //     try
        //     {
        //         var likes = await _context.Likes
        //             .Where(x => x.)
        //     }
        //     catch (Exception e)
        //     {
        //         _logger.LogError(e.Message);
        //     }
        //
        //     return null;
        // }
    }
}