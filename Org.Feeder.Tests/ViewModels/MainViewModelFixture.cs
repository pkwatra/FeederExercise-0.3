using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Org.Feeder.Model;
using Org.Feeder.App.ViewModels;
using System.Collections.Generic;
using NSubstitute;
using Org.Feeder.App.Framework.Startup;
using Org.Feeder.App.Framework.Navigate;
using Org.Feeder.Service;
using System.Collections.ObjectModel;

namespace Org.Feeder.Tests.ViewModels
{
    [TestClass]
    public class MainViewModelFixture
    {
        private List<PostSummary> _posts;
        private MainViewModel _viewModel;
        private PostSummaryResult _postSummarySample;       
        private INavigator _navigator;

        [TestInitialize]
        public void Init()
        {
            _posts = (from id in Enumerable.Range(1, 12)
                      select new PostSummary(id, "Post " + id)).ToList();

            _navigator = Substitute.For<INavigator>();
            var dataService = Substitute.For<IDataService>();
            _postSummarySample = Substitute.For<PostSummaryResult>();           
            _postSummarySample.PostSummary = _posts;         

            _postSummarySample.Error = string.Empty;
            _postSummarySample.ErrorType = string.Empty;

            dataService.GetPostSummary().Returns(x => _postSummarySample);
            _viewModel = Substitute.For<MainViewModel>(_navigator, dataService);

        }

        [TestMethod]
        public void LoadPostSummaryTest()
        {
            _viewModel.LoadedCommand.Execute(null);
            _viewModel.GetPostRecords().Wait();

            Assert.AreEqual(_postSummarySample.PostSummary.Count, _viewModel.InitialPosts.Count);

            Assert.AreEqual(_postSummarySample.PostSummary.Count, _viewModel.Posts.Count);

        }


        //Test for Filte post after execute FilterCommand
        [TestMethod]
        public void FilteringTest()
        {
            //Arrange
            _viewModel.InitialPosts = _posts;
            _viewModel.Posts = new ObservableCollection<PostSummary>(_posts);

            //Act
            _viewModel.FilterCommand.Execute("Post 1");

            //Act
            CollectionAssert.AreEquivalent(
                new[] { "Post 1", "Post 10", "Post 11", "Post 12" },
                _viewModel.Posts.Select(x => x.Title).ToArray());
        }

        [TestMethod]
        public void NavigateToErrorPageTest()
        {
            //Arrange
            _postSummarySample.Error = "Some Error!";
            _postSummarySample.ErrorType = "Error Type!";

            //Act
            _viewModel.LoadedCommand.Execute(null); 

            //Assert
            Assert.AreEqual(_viewModel.Posts.Count, 0);

            //Not able to test Navigate to Error Page here, However, tested on Navigation Page
        }


        //Test for Navigate Comment View after execute SelectCommand
        [TestMethod]
        public void SelectCommandExecuteTest()
        {
            //Act
            _viewModel.SelectCommand.Execute(_posts.First());

            //Assert
            _navigator.Received().GoToComment(_posts.First());
        }

        //Test for SelectedPost after execute SelectCommand
        [TestMethod]
        public void SelectingPostTest()
        {
            //Act
            var selectedPost = _posts.Skip(5).First();
            _viewModel.SelectCommand.Execute(selectedPost);

            Assert.AreEqual(6, selectedPost.PostId);
        }
    }
}