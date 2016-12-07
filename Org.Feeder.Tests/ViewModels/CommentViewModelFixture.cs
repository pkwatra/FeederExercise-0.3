using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Org.Feeder.App.Framework.Event;
using Org.Feeder.App.Framework.Navigate;
using Org.Feeder.Model;
using Org.Feeder.Model.Service;

using Org.Feeder.Service;
using Org.Feeder.App.ViewModels;
using Prism.Events;

using System.Linq;

namespace Org.Feeder.Tests.ViewModels
{
    [TestClass]
    public class CommentViewModelFixture
    {
        private int _userId = 1;
        private IDataService _dataService;
        private PostSummary _postSummary;       
        private CommentViewModel _viewModel;        
        private PostCommentResult _postCommentResult;      
        private INavigator _navigator;

        [TestInitialize]
        public void Init()
        {
            _navigator = Substitute.For<INavigator>();
            var showLoadingEvent = Substitute.For<ShowLoadingEvent>();
            _dataService = Substitute.For<IDataService>();
            var eventAggregator = Substitute.For<IEventAggregator>();
            eventAggregator.GetEvent<ShowLoadingEvent>().Returns(showLoadingEvent);
            _postSummary = Substitute.For<PostSummary>(1, "Post 1");
            _postCommentResult = InitPostCommentResult();
            _dataService.GetPostComment(_postSummary.PostId).Returns(x => _postCommentResult);
            _viewModel = Substitute.For<CommentViewModel>(_navigator, _dataService, eventAggregator, _postSummary);
        }   
            
          

        [TestMethod]
        public void LoadPostCommentsTest()
        {
            //Act
            _viewModel.LoadedCommand.Execute(null);
            _viewModel.GetPostComments().Wait();

            //Assert
            Assert.AreEqual(_postCommentResult.Post.Title, _viewModel.Post.Title);
            Assert.AreEqual(_userId, _viewModel.User.UserId);
            Assert.AreEqual(_postCommentResult.Comments.Count, _viewModel.Comments.Count);

        }
        
        [TestMethod]
        public void GoBackToMainScreenTest()
        {
            _viewModel.GoBackCommand.Execute(null);

            _navigator.Received().GoToMain();
        }


        [TestMethod]
        public void NavigateToErrorPageTest()
        {
            //Arrange
            _postCommentResult.Error = "Some Error!";
            _postCommentResult.ErrorType = "Error Type!";

            //Act
            _viewModel.LoadedCommand.Execute(null);
            _viewModel.GetPostComments().Wait();

            //Assert
            Assert.IsNull(_viewModel.Post);
            Assert.IsNull(_viewModel.User);
            Assert.IsNull(_viewModel.Comments);

            //Not able to test Navigate to Error Page here, However, tested on Navigation Page
        }

        //Initialize Data Service Post Comment Result
        private PostCommentResult InitPostCommentResult()
        {           
             
            var postCommentResult = Substitute.For<PostCommentResult>();

            postCommentResult.Post = Substitute.For<Post>(_postSummary.PostId, _postSummary.Title, "Post Body", _userId);

            postCommentResult.Comments = (from id in Enumerable.Range(1, 12)
                                           select new Comment(_postSummary.PostId ,"Comment Text" + id, "Comment Name " + id, "Comment" + id + "@email.com" )).ToList();

            postCommentResult.User = Substitute.For<FeederUser>(_userId, "User Name" + _userId, "User"+_userId+"@email.com", "/Resources/Images/background.jpeg");

            postCommentResult.Error = string.Empty;

            postCommentResult.ErrorType = string.Empty;

            return postCommentResult;
        }
    }
}
