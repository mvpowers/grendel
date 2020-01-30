namespace GrendelData
{
    public interface IAppSettings
    {
        string Secret { get; }
    }
    public class AppSettings : IAppSettings
    {
        public string Secret { get; set; }
    }
}