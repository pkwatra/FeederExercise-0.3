using Org.Feeder.Model;
using System;

namespace Org.Feeder.App.Framework.Navigate
{
    public interface INavigator
    {
        void GoToIntro();
        void GoToMain();
        void GoToComment(PostSummary postSummary);
        void ShowError(string title, string message, Action recoveryAction);
    }
}