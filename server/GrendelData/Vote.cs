namespace GrendelData
{
    public class Vote
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        
        public int VoteOptionId { get; set; }
        public VoteOption VoteOption { get; set; }
    }
}