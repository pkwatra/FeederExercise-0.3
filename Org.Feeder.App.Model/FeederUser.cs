namespace Org.Feeder.Model
{
    public class FeederUser
    {
        public FeederUser(int userId, string name, string email, string imageUrl)
        {
            UserId = userId;
            Name = name;
            Email = email;
            ImageUrl = imageUrl;
        }
        public int UserId { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string ImageUrl { get; private set; }

    }
}
