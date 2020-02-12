using System.Collections.Generic;
using GrendelData.Votes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GrendelData.VoteOptions
{
    public class VoteOption
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        
        public List<Vote> Votes { get; set; }

        public VoteOption()
        {
            Votes = new List<Vote>();
        }
    }

    public class VoteOptionConfiguration : IEntityTypeConfiguration<VoteOption>
    {
        public void Configure(EntityTypeBuilder<VoteOption> builder)
        {
            builder
                .Property(x => x.IsActive)
                .HasDefaultValue(true);
        }
    }
}