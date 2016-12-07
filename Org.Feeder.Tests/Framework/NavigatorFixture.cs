using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Org.Feeder.App.Framework;
using Org.Feeder.App.ViewModels;
using Org.Feeder.App.Models;
using System.Linq;
using System.Collections.Generic;

namespace Org.Feeder.Tests.Framework
{
    [TestClass]
    public class NavigatorFixture
    {
        private IContentHostViewModel _appShell;
        private INavigator _navigator;

        public NavigatorFixture()
        {
            _appShell = Substitute.For<IContentHostViewModel>();
            _navigator = Substitute.For<Navigator>(_appShell);
        } 

        [TestMethod]
        public void GoingToIntro()
        {
            _navigator.GoToIntro();

            Assert.IsInstanceOfType(_appShell.Content, typeof(IntroViewModel));
        }

        [TestMethod]
        public void ShowingError()
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
        public void GoingToMain()
        {
           
            _navigator.GoToMain();

            Assert.IsInstanceOfType(_appShell.Content, typeof(MainViewModel));
           
        }

        [TestMethod]        
        public void GoToCommentTest()
        {
            _navigator.GoToComment();
            Assert.IsInstanceOfType(_appShell.Content, typeof(CommentViewModel));
        }

    }
}
