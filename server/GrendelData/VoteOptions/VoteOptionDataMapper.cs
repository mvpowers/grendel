using System;
using System.Collections.Generic;

namespace GrendelData.VoteOptions
{
    public static class VoteOptionDataMapper
    {
        public static VoteOption ToVoteOption(this VoteOptionCreateRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            
            return new VoteOption()
            {
                Name = request.Name
            };
        }

        public static VoteOptionView ToVoteOptionView(this VoteOption voteOption)
        {
            if (voteOption == null) throw new ArgumentNullException(nameof(voteOption));
            
            return new VoteOptionView()
            {
                Id = voteOption.Id,
                Name = voteOption.Name
            };
        }

        public static List<VoteOptionView> ToVoteOptionView(this List<VoteOption> voteOptions)
        {
            if (voteOptions == null) throw new ArgumentNullException(nameof(voteOptions));

            return voteOptions.ConvertAll(x => x.ToVoteOptionView());
        }
    }
}