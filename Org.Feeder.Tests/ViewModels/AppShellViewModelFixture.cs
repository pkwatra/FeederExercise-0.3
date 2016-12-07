using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Org.Feeder.App.Framework.Event;
using Org.Feeder.App.Framework.Startup;
using Org.Feeder.App.ViewModels;
using Prism.Events;
using Org.Feeder.Tests.Extension;

namespace Org.Feeder.Tests.ViewModels
{
    [TestClass]
    public class AppShellViewModelFixture
    {
        private IContentHostViewModel _appShellViewModel;
        private ShowLoadingEvent _ShowLoadingEvent;

        [TestInitialize]
        public void Init()
        {
            _ShowLoadingEvent = Substitute.For<ShowLoadingEvent>();
            var eventAggregator = Substitute.For<IEventAggregator>();

            eventAggregator.GetEvent<ShowLoadingEvent>().Returns(_ShowLoadingEvent);

            _appShellViewModel = Substitute.For<AppShellViewModel>(eventAggregator);            
             
        }

        [TestMethod]
        public void ShowLoadingIndicatorTest()
        {
            var isLoading = true;
            _ShowLoadingEvent.Publish(isLoading);

            var fired = _appShellViewModel.IsPropertyChangedFired(() =>
            {
                _appShellViewModel.IsLoading = isLoading;
            }, nameof(_appShellViewModel.IsLoading));

            Assert.IsTrue(_appShellViewModel.IsLoading);
        }


        [TestMethod]
        public void HideLoadingIndicatorTest()
        {
            var isLoading = false;
            _ShowLoadingEvent.Publish(isLoading);

            var fired = _appShellViewModel.IsPropertyChangedFired(() =>
            {
                _appShellViewModel.IsLoading = isLoading;
            }, nameof(_appShellViewModel.IsLoading));

            Assert.IsFalse(_appShellViewModel.IsLoading);
        }
    }
}
