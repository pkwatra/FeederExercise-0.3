using System;
using System.Collections.Generic;
using Org.Feeder.Model;
using Org.Feeder.Model.DataProvider;
using System.Linq;
using Org.Feeder.Model.Service;
using System.Configuration;
using System.Threading.Tasks;

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
                    provider = null;
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
                result = GetPostSummaryAsync();

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

                result = GetPostCommentAsync(postId);
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


        //Will work as Async
        private PostSummaryResult GetPostSummaryAsync()
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

        }

        //Will work as Async
        public PostCommentResult GetPostCommentAsync(int postId)
        {
            PostCommentResult postCommentResult = new PostCommentResult();

            if (provider != null)
            {
                postCommentResult.Post = provider.GetPost(postId);
                var users = provider.GetUsers();
                postCommentResult.User = users.SingleOrDefault(x => x.UserId == postCommentResult.Post.UserId);
                var comments = provider.GetComment(postId);
                postCommentResult.Comments = comments.ToList();
            }

            return postCommentResult;
        }
    }
}
