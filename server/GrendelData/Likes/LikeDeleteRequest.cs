using System.ComponentModel.DataAnnotations;

namespace GrendelData.Likes
{
    public class LikeDeleteRequest
    {
        [Required]
        public int VoteId { get; set; }
    }
}