using System.ComponentModel.DataAnnotations;

namespace GrendelData.Users
{
    public class UserAuthRequest
    {
        [Required]
        public long Phone { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}