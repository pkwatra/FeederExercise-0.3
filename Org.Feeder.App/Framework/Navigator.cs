using System;
using System.Linq;
using Org.Feeder.App.ViewModels;
using Org.Feeder.FeederDb;
using PostSummary = Org.Feeder.App.Models.PostSummary;
using Autofac.Core;
using Autofac;

namespace Org.Feeder.App.Framework
{
    public class Navigator : INavigator
    {
        private readonly IContentHostViewModel _appShell;       

        public Navigator(IContentHostViewModel appShell)
        {
            _appShell = appShell;           
        }

        public void GoToIntro()
        {
            Display(new IntroViewModel(this));
        }

        public void GoToMain()
        {
            Display(new MainViewModel(this));
        }

        public void GoToComment()
        {
            Display(new CommentViewModel());
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