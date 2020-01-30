using GrendelCommon;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GrendelData
{
    public class GrendelContextFactory : IDesignTimeDbContextFactory<GrendelContext>
    {
        public GrendelContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<GrendelContext>();
            optionsBuilder.UseNpgsql(DatabaseSettings.GetConnectionString());
            
            return new GrendelContext(optionsBuilder.Options);
        }
    }
}