namespace GrendelData
{
    public interface IAppSettings
    {
        string Secret { get; }
        int VoteSessionDurationMinutes { get; }
    }
    public class AppSettings : IAppSettings
    {
        public string Secret { get; set; }
        public int VoteSessionDurationMinutes { get; set; }
    }
}