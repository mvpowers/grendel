using GrendelData.Questions;
using GrendelData.Users;
using GrendelData.VoteOptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GrendelData.Votes
{
    public class Vote
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        
        public int UserId { get; set; }
        public User User { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        
        public int VoteOptionId { get; set; }
        public VoteOption VoteOption { get; set; }
    }

    public class VoteConfiguration : IEntityTypeConfiguration<Vote>
    {
        public void Configure(EntityTypeBuilder<Vote> builder)
        {
            builder.HasIndex(x => new {x.UserId, x.QuestionId}).IsUnique();
        }
    }
}