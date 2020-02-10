namespace GrendelData.Users
{
    public class UserSessionView
    {
        public int Id { get; set; }
        public bool HasVotingExpired { get; set; }
        public bool HasActiveVote { get; set; }
    }
}