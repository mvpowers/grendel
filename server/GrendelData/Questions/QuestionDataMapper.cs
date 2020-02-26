using System.Collections.Generic;

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

        public static QuestionView ToQuestionView(this Question question, int voteSessionDurationMinutes)
        {
            return new QuestionView()
            {
                Id = question.Id,
                Inquiry = question.Inquiry,
                TimeAsked = question.TimeAsked,
                TimeVotingExpires = question.TimeAsked?.AddMinutes(voteSessionDurationMinutes)
            };
        }

        public static QuestionQueueView ToQuestionQueueView(this Question question)
        {
            return new QuestionQueueView()
            {
                Id = question.Id,
                Inquiry = question.Inquiry
            };
        }

        public static List<QuestionQueueView> ToQuestionQueueView(this List<Question> questions)
        {
            return questions.ConvertAll(x => x.ToQuestionQueueView());
        }
    }
}