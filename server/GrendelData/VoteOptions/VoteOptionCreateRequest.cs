using System.ComponentModel.DataAnnotations;

namespace GrendelData.VoteOptions
{
    public class VoteOptionCreateRequest
    {
        [Required]
        public string Name { get; set; }
    }
}