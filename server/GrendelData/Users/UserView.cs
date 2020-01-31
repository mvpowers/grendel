namespace GrendelData.Users
{
    public class UserView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long Phone { get; set; }
        public string Token { get; set; }
        public bool IsAdmin { get; set; }
    }
}