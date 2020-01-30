using System;

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
    }
}