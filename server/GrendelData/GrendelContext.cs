using GrendelData.Questions;
using GrendelData.Users;
using GrendelData.VoteOptions;
using GrendelData.Votes;
using Microsoft.EntityFrameworkCore;

namespace GrendelData
{
    public class GrendelContext : DbContext
    {
        public DbSet<Question> Questions { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<VoteOption> VoteOptions { get; set; }
        public DbSet<User> Users { get; set; }
        
        public GrendelContext(DbContextOptions<GrendelContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new QuestionConfiguration());
            builder.ApplyConfiguration(new VoteConfiguration());
            builder.ApplyConfiguration(new VoteOptionConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
        }
    }
}