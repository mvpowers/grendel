using System;

namespace GrendelData.Votes
{
    public static class VoteDataMapper
    {
        public static Vote ToVote(this VoteCreateRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            
            return new Vote()
            {
                Comment = request.Comment,
                QuestionId = request.QuestionId,
                VoteOptionId = request.VoteOptionId,
                UserId = 1
            };
        }
    }
}