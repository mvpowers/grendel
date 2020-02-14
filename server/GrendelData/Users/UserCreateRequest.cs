using System.ComponentModel.DataAnnotations;

namespace GrendelData.Users
{
    public class UserCreateRequest
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public long Phone { get; set; }
    }
}