using Org.Feeder.Model;
using System.Collections.Generic;

namespace Org.Feeder.Model.DataProvider
{
    public interface IDataProvider
    {
        IEnumerable<Comment> GetComment(int postId);
        Post GetPost(int PostId);
        IEnumerable<PostSummary> GetPostSummary();
        IEnumerable<FeederUser> GetUsers();
    }
}
