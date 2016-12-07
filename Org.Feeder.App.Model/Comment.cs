namespace Org.Feeder.Model
{
    public class Comment
    {
        public Comment(int postId, string text, string commentName, string commentEmail)
        {
            PostId = postId;
            Text = text;
            CommentName = commentName;
            CommentEmail = commentEmail;
        }
        public int PostId { get; private set; }
        public string Text { get; private set; }
        public string CommentName { get; private set; }
        public string CommentEmail { get; private set; }
    }
}
