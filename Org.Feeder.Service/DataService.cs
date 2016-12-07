using System;
using Org.Feeder.Model.DataProvider;
using System.Linq;
using Org.Feeder.Model.Service;
using System.Configuration;
using Org.Feeder.DataProvider;
using System.Threading.Tasks;
using System.Collections.Generic;
using Org.Feeder.Model;

namespace Org.Feeder.Service
{
    public class DataService : IDataService
    {
        IDataProvider provider = null;
        public DataService()
        {
            //Default value is "Sample"
            var serverType = GetDataAccessType();

            switch (serverType)
            {
                case DataAccessType.Sample:
                    provider = new SampleDataProvider.SampleDataProvider();
                    break;
                case DataAccessType.SQL:
                    provider = new DataProvider.SQLDataProvider(GetConnecctionString());
                    break;
            }
        }

        #region Configuration Value from App Config
        private DataAccessType GetDataAccessType()
        {
            var dataProvider = ConfigurationManager.AppSettings["DataProvider"];

            if (!string.IsNullOrEmpty(dataProvider))
            {
                return dataProvider.ToLower() == "sql" ? DataAccessType.SQL : DataAccessType.Sample;
            }
            else
                return DataAccessType.Sample;
        }

        private string GetConnecctionString()
        {
            var connectionString = ConfigurationManager.AppSettings["ConnectionString"];

            if (!string.IsNullOrEmpty(connectionString))
            {
                return connectionString.ToLower();
            }
            else
                return "local";
        }
        #endregion

        public PostSummaryResult GetPostSummary()
        {
            PostSummaryResult result = new PostSummaryResult();
            try
            {
                result = GetPostSummaryAsync().Result;

            }
            catch (ArgumentException ae)
            {
                //We can use enum or other way to define error type
                result.ErrorType = "Database Error";
                result.Error = ae.InnerException.Message;
            }
            catch (Exception ex)
            {
                //We can use enum or other way to define error type
                result.ErrorType = "Application Error";
                result.Error = ex.Message;
            }

            return result;

        }

        public PostCommentResult GetPostComment(int postId)
        {
            PostCommentResult result = new PostCommentResult();
            try
            {

                result = GetPostCommentAsync(postId).Result;
            }
            catch (AggregateException ae)
            {
                //We can use enum or other way to define error type
                result.ErrorType = "Database Error";
                foreach (var exception in ae.Flatten().InnerExceptions)
                {
                    result.Error += exception.Message;
                }
            }
            catch (Exception ex)
            {
                //We can use enum or other way to define error type
                result.ErrorType = "Application Error";
                result.Error = ex.Message;
            }

            return result;
        }

        private Task<PostSummaryResult> GetPostSummaryAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                PostSummaryResult postSummary = new PostSummaryResult();
                if (provider != null)
                {
                    var result = provider.GetPostSummary();
                    if (result != null)
                    {
                        postSummary.PostSummary = result.ToList();
                    }
                }

                return postSummary;
            });           

        }

        public Task<PostCommentResult> GetPostCommentAsync(int postId)
        {

            return Task.Factory.StartNew(() =>
            {
                PostCommentResult postCommentResult = new PostCommentResult();

                if (provider != null)
                {
                    var post = Task.Factory.StartNew(() =>
                    {
                        return provider.GetPost(postId);

                    }, TaskCreationOptions.AttachedToParent);

                    postCommentResult.Post = post.Result;

                    var users = post.ContinueWith((postResult) =>
                    {
                        return provider.GetUsers();

                    }, TaskContinuationOptions.AttachedToParent);

                    postCommentResult.User = users.Result.SingleOrDefault(x => x.UserId == postCommentResult.Post.UserId);

                    var comments = Task.Factory.StartNew(() =>
                    {
                        return provider.GetComment(postId);

                    }, TaskCreationOptions.AttachedToParent);


                    postCommentResult.Comments = comments.Exception == null ? comments.Result.ToList() : new List<Comment>();

                }

                return postCommentResult;
            });

        }
    }
}
