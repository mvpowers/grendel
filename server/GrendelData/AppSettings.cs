namespace GrendelData
{
    public interface IAppSettings
    {
        string AppUrl { get; }
        string Secret { get; }
        int VoteSessionDurationMinutes { get; }
        string TwilioSid { get; }
        string TwilioAuthToken { get; }
        string TwilioSendNumber { get; }
    }
    public class AppSettings : IAppSettings
    {
        public string AppUrl { get; set;  }
        public string Secret { get; set; }
        public int VoteSessionDurationMinutes { get; set; }
        public string TwilioSid { get; set; }
        public string TwilioAuthToken { get; set; }
        public string TwilioSendNumber { get; set; }
    }
}