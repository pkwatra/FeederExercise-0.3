using System;
using Org.Feeder.App.ViewModels;
using Org.Feeder.App.Framework.Startup;
using Org.Feeder.Service;
using Org.Feeder.Model;

namespace Org.Feeder.App.Framework.Navigate
{
    public class Navigator : INavigator
    {
        private readonly IContentHostViewModel _appShell;     
        private IDataService _dataService;

        public Navigator(IContentHostViewModel appShell,                         
                         IDataService dataService)
        {
            _appShell = appShell;            
            _dataService = dataService;
        }

        public void GoToIntro()
        {
            Display(new IntroViewModel(this));
        }

        public void GoToMain()
        {
            Display(new MainViewModel(this, _dataService));
        }

        public void GoToComment(PostSummary postSummary)
        {
            Display(new CommentViewModel(this, _dataService, postSummary));
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