using System.ComponentModel.DataAnnotations;

namespace GrendelData.Votes
{
    public class VoteCreateRequest
    {
        public string Comment { get; set; }
        
        [Required]
        public int QuestionId { get; set; }
        
        [Required]
        public int VoteOptionId { get; set; }
    }
}