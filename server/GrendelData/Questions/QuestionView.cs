using System;

namespace GrendelData.Questions
{
    public class QuestionView
    {
        public int Id { get; set; }
        public string Inquiry { get; set; }
        public DateTime? TimeAsked { get; set; }
        public bool? IsSessionActive { get; set; }
    }
}