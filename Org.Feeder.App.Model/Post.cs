namespace Org.Feeder.Model
{
    public class Post
    {
        public Post(int id, string title, string body, int userId)
        {
            Id = id;
            Title = title;
            Body = body;
            UserId = userId;
        }
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Body { get; private set; }
        public int UserId { get; private set; }
    }
}
