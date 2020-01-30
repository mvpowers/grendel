using System.ComponentModel.DataAnnotations;

namespace GrendelData.Questions
{
    public class QuestionCreateRequest
    {
        [Required]
        public string Inquiry { get; set; }
    }
}