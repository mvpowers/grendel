using System.ComponentModel.DataAnnotations;

namespace GrendelData.Users
{
    public class UserInfoRequest
    {
        [Required]
        public string Token { get; set; }
    }
}