using System.ComponentModel.DataAnnotations;

namespace GrendelData.Likes
{
    public class LikeCreateRequest
    {
        [Required]
        public int VoteId { get; set; }
    }
}