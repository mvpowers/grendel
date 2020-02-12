using GrendelData.Users;
using GrendelData.Votes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GrendelData.Likes
{
    public class Like
    {
        public int Id { get; set; }
        
        public int VoteId { get; set; }
        public Vote Vote { get; set; }
        
        public int UserId { get; set; }
        public User User { get; set; }
    }

    public class LikeConfiguration : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> builder)
        {
            builder.HasIndex(x => new {x.VoteId, x.UserId});
        }
    }
}