using System.ComponentModel.DataAnnotations;

namespace GrendelData.Users
{
    public class UserTokenRequest
    {
        [Required]
        public long Phone { get; set; }
    }
}