using Microsoft.EntityFrameworkCore;

namespace GrendelData
{
    public class GrendelContext : DbContext
    {
        public DbSet<Question> Questions { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<VoteOption> VoteOptions { get; set; }
        
        public GrendelContext(DbContextOptions<GrendelContext> options) : base(options)
        {
        }
    }
}