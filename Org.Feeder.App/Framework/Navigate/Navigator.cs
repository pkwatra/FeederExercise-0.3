using System;
using Org.Feeder.App.ViewModels;
using Org.Feeder.App.Framework.Startup;
using Org.Feeder.Service;
using Org.Feeder.Model;
using Prism.Events;

namespace Org.Feeder.App.Framework.Navigate
{
    public class Navigator : INavigator
    {
        private readonly IContentHostViewModel _appShell;     
        private IDataService _dataService;
        private IEventAggregator _eventAggregator;

        public Navigator(IContentHostViewModel appShell,                         
                         IDataService dataService,
                         IEventAggregator eventAggregator)
        {
            _appShell = appShell;            
            _dataService = dataService;
            _eventAggregator = eventAggregator;
        }

        public void GoToIntro()
        {
            Display(new IntroViewModel(this));
        }

        public void GoToMain()
        {
            Display(new MainViewModel(this, _dataService, _eventAggregator));
        }

        public void GoToComment(PostSummary postSummary)
        {
            Display(new CommentViewModel(this, _dataService, _eventAggregator, postSummary));
        }

        public void ShowError(string title, string message, Action recoveryAction)
        {
            Display(new ErrorViewModel(title, message, recoveryAction));
        }

        private void Display<TViewModel>(TViewModel viewModel) where TViewModel : IViewModel
        {
            _appShell.Content = viewModel;
        }
    }
}