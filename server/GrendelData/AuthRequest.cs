using System.ComponentModel.DataAnnotations;

namespace GrendelData
{
    public class AuthRequest
    {
        [Required]
        public long Phone { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}