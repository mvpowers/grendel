using System;
using System.Collections.Generic;
using GrendelData.Users;
using GrendelData.Votes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GrendelData.Questions
{
    public class Question
    {
        public int Id { get; set; }
        public string Inquiry { get; set; }
        public DateTime? TimeAsked { get; set; }
        public bool? IsQuestionActive { get; set; }
        public bool? IsSessionActive { get; set; }
        
        public int UserId { get; set; }
        public User User { get; set; }
        
        public List<Vote> Votes { get; set; }

        public Question()
        {
            Votes = new List<Vote>();
        }
    }

    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder
                .Property(x => x.IsQuestionActive)
                .HasDefaultValue(false);
            
            builder
                .Property(x => x.IsSessionActive)
                .HasDefaultValue(false);
        }
    }
}