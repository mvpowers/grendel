namespace GrendelData.Users
{
    public class UserSessionView
    {
        public int Id { get; set; }
        public bool HasActiveSession { get; set; }
        public bool HasActiveVote { get; set; }
    }
}