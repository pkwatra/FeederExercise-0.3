using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Org.Feeder.App.Framework;
using Org.Feeder.App.ViewModels;

namespace Org.Feeder.Tests.Framework
{
    [TestClass]
    public class BootstrapperFixture
    {
        [TestMethod]
        public void Initializing()
        {
            // Arrange
            var hostWindow = Substitute.For<IWindow>();
            var appShell =Substitute.For<IContentHostViewModel>();
            var factory = Substitute.For<HostWindowFactory>();
            factory.CreateHostWindow(appShell).Returns(hostWindow);
            var bootstrapper = new Bootstrapper(factory);
            var navigator = Substitute.For<Navigator>(appShell);

            // Act
            bootstrapper.Initialize(appShell, navigator);

            // Assert
            hostWindow.Received().Show();
            Assert.IsInstanceOfType(appShell.Content, typeof(IntroViewModel));
        }
    }
}