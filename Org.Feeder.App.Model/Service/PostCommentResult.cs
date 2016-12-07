using System.Collections.Generic;

namespace Org.Feeder.Model.Service
{
    public class PostCommentResult
    {
        public Post Post { get; set; }
        public FeederUser User { get; set; }
        public List<Comment> Comments { get; set; }
        public string Error { get; set; }
        public string ErrorType { get; set; }
    }
}
