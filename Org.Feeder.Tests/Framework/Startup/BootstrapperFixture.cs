using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Org.Feeder.App.Framework.Navigate;
using Org.Feeder.App.Framework.Startup;
using Org.Feeder.App.ViewModels;
using Org.Feeder.Service;
using Prism.Events;

namespace Org.Feeder.Tests.Framework.Startup
{
    [TestClass]
    public class BootstrapperFixture
    {
        [TestMethod]
        public void Initializing()
        {
            // Arrange
            var hostWindow = Substitute.For<IWindow>();
            var eventAggregator = Substitute.For<IEventAggregator>();
            var dataService = Substitute.For<IDataService>();
            var viewModel = Substitute.For<IContentHostViewModel>();
            var factory = Substitute.For<HostWindowFactory>();
            factory.CreateHostWindow(viewModel).Returns(hostWindow);
            var bootstrapper = new Bootstrapper(factory);
            var navigator = Substitute.For<Navigator>(viewModel, dataService, eventAggregator);

            // Act
            bootstrapper.Initialize(viewModel, navigator);

            // Assert
            hostWindow.Received().Show();
            Assert.IsInstanceOfType(viewModel.Content, typeof(IntroViewModel));
        }
    }
}