using System;
using System.Collections.Generic;

namespace GrendelData.Votes
{
    public static class VoteDataMapper
    {
        public static Vote ToVote(this VoteCreateRequest request, int userId)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            
            return new Vote()
            {
                Comment = request.Comment,
                QuestionId = request.QuestionId,
                VoteOptionId = request.VoteOptionId,
                UserId = userId
            };
        }

        public static VoteView ToVoteView(this Vote vote)
        {
            if (vote == null) throw new ArgumentNullException(nameof(vote));
            
            return new VoteView()
            {
                Id = vote.Id,
                Comment = vote.Comment,
                VoteOptionId = vote.VoteOptionId
            };
        }
        
        public static List<VoteView> ToVoteView(this List<Vote> votes)
        {
            if (votes == null) throw new ArgumentNullException(nameof(votes));
            return votes.ConvertAll(x => x.ToVoteView());
        }
    }
}