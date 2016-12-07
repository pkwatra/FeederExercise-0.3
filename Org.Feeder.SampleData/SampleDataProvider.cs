using System;
using System.Collections.Generic;
using Org.Feeder.Model;
using Org.Feeder.Model.DataProvider;
using System.Linq;

namespace Org.Feeder.SampleDataProvider
{
    public class SampleDataProvider : IDataProvider
    {
        private IEnumerable<Post> _posts;

        public SampleDataProvider()
        {
            _posts = (from id in Enumerable.Range(1, 12)
                      select new Post(id, "Post " + id, "Body " + id, id));

        }
        public IEnumerable<Comment> GetComment(int postId)
        {
            return (from id in Enumerable.Range(1, 12)
                    select new Comment(id, "Text " + id, "Commen Name " + id, "CommenEmail" + id + "@email.com"));
        }

        public Post GetPost(int postId)
        {
            return _posts.SingleOrDefault(x => x.Id == postId);
        }

        public IEnumerable<PostSummary> GetPostSummary()
        {
            return _posts.ToList().Select(x => new PostSummary(x.Id, x.Title));
        }

        public IEnumerable<FeederUser> GetUsers()
        {
            return (from id in Enumerable.Range(1, 12)
                    select new FeederUser(id, "User Name " + id, "User" + id + "@email.com", "/Resources/Images/background.jpeg"));
        }
    }
}
