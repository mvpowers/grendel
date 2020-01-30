using System;

namespace GrendelData.Questions
{
    public static class QuestionDataMapper
    {
        public static Question ToQuestion(this QuestionCreateRequest request, int userId)
        {
            return new Question()
            {
                Inquiry = request.Inquiry,
                UserId = userId
            };
        }
    }
}