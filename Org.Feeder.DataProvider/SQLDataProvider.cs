using System;
using System.Collections.Generic;
using Org.Feeder.Model.DataProvider;
using Org.Feeder.FeederDb;
using System.Linq;

namespace Org.Feeder.DataProvider
{
    public class SQLDataProvider : IDataProvider
    {
        private Database _database;

        public SQLDataProvider(string connectionString)
        {
            _database = new Database(connectionString);
        }

        public IEnumerable<Model.Comment> GetComment(int postId)
        {
            try
            {
                var comments = _database.GetComments(postId);
                if (comments == null) { return null; }

                return comments.Select(cmt => new Model.Comment(cmt.PostId,
                                                                cmt.Text,
                                                                cmt.CommenterName,
                                                                cmt.CommenterEmail));
            }
            catch (Exception)
            {
                throw;
            }


        }

        public Model.Post GetPost(int postId)
        {
            try
            {
                var post = _database.GetPost(postId);
                if (post == null) { return null; }

                return new Model.Post(post.Id, post.Title, post.Body, post.UserId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Model.PostSummary> GetPostSummary()
        {
            try
            {
                var posts = _database.GetPostSummaries();
                if (posts == null) { return null; }

                return posts.Select(post => new Model.PostSummary(post.Id,
                                                                  post.Title));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Model.FeederUser> GetUsers()
        {
            try
            {
                var users = _database.GetUsers();
                if (users == null) { return null; }

                return users.Select(user => new Model.FeederUser(user.UserId,
                                                                 user.Name,
                                                                 user.Email,
                                                                 user.ImageUrl));

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
