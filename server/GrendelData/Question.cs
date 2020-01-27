using System;

namespace GrendelData
{
    public class Question
    {
        public int Id { get; set; }
        public string Inquiry { get; set; }
        public DateTime? TimeAsked { get; set; }
        public bool IsActive { get; set; }
    }
}