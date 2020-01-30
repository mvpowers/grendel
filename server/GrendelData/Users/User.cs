using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GrendelData.Questions;
using GrendelData.Votes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GrendelData.Users
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public string PasswordResetToken { get; set; }
        public bool IsAdmin { get; set; }

        [Range(999999999, 1000000000)]
        public long Phone { get; set; }
        
        public List<Vote> Votes { get; set; }
        public List<Question> Questions { get; set; }
    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .Property(x => x.IsAdmin)
                .HasDefaultValue(false);
        }
    }
}