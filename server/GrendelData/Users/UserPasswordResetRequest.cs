using System.ComponentModel.DataAnnotations;

namespace GrendelData.Users
{
    public class UserPasswordResetRequest
    {
        [Required]
        public string NewPassword { get; set; }
        
        [Required]
        public string NewPasswordConfirm { get; set; }
        
        [Required]
        public string UserResetToken { get; set; }
    }
}