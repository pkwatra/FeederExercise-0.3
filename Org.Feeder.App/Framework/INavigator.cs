using System;

namespace Org.Feeder.App.Framework
{
    public interface INavigator
    {
        void GoToIntro();
        void GoToMain();
        void GoToComment();
        void ShowError(string title, string message, Action recoveryAction);
    }
}