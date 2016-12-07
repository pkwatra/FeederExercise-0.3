using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Org.Feeder.App.Framework.Navigate;
using Org.Feeder.Model;
using Org.Feeder.App.ViewModels;
using System.Linq;
using System.Collections.Generic;

namespace Org.Feeder.Tests.ViewModels
{
    [TestClass]
    public class IntroViewModelFixture
    {        
        [TestMethod]
        public void Starting()
        {
            var navigator = Substitute.For<INavigator>();
            var viewModel = new IntroViewModel(navigator);

            viewModel.StartCommand.Execute(null);

            // NSubstitute allows you to assert the navigator was called with the method `Received()`
            navigator.Received().GoToMain();
        }
    }
}