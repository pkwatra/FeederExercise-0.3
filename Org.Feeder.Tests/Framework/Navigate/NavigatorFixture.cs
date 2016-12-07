using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Org.Feeder.App.ViewModels;
using Org.Feeder.App.Framework.Startup;
using Org.Feeder.App.Framework.Navigate;
using Org.Feeder.Service;
using Org.Feeder.Model;
using Prism.Events;

namespace Org.Feeder.Tests.Framework.Navigate
{
    [TestClass]
    public class NavigatorFixture
    {
        private IContentHostViewModel _appShell;
        private INavigator _navigator;
        private IEventAggregator _eventAggregator;
        private IDataService _dataService;

        [TestInitialize]
        public void Initialize()
        {
            _appShell = Substitute.For<IContentHostViewModel>();
            _eventAggregator = Substitute.For<IEventAggregator>();
            _dataService = Substitute.For<IDataService>();
            _navigator = Substitute.For<Navigator>(_appShell, _dataService, _eventAggregator);
        }

        [TestMethod]
        public void GoingToIntroTest()
        {
            _navigator.GoToIntro();

            Assert.IsInstanceOfType(_appShell.Content, typeof(IntroViewModel));
        }

        [TestMethod]
        public void ShowingErrorTest()
        {
            const string title = "Something descriptive to the user", message = "And a message so they know what happened.";
            var recoveryAction = Substitute.For<Action>();

            _navigator.ShowError(title, message, recoveryAction);

            var errorViewModel = (ErrorViewModel)_appShell.Content;
            Assert.AreEqual(title, errorViewModel.Title);
            Assert.AreEqual(message, errorViewModel.Message);

            errorViewModel.DismissCommand.Execute(null);
            recoveryAction.Received().Invoke();
        }

        [TestMethod]
        public void GoingToMainTest()
        {

            _navigator.GoToMain();

            Assert.IsInstanceOfType(_appShell.Content, typeof(MainViewModel));

        }

        [TestMethod]
        public void GoToCommentTest()
        {
            var postSummary = Substitute.For<PostSummary>(1, "Post 1");

            _navigator.GoToComment(postSummary);
            Assert.IsInstanceOfType(_appShell.Content, typeof(CommentViewModel));
        }

    }
}
