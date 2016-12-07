using Org.Feeder.App.Framework.Navigate;

namespace Org.Feeder.App.Framework.Startup
{
    public class Bootstrapper
    {
        private readonly HostWindowFactory _hostWindowFactory;
        private IWindow _mainWindow;

        public Bootstrapper(HostWindowFactory hostWindowFactory)
        {
            _hostWindowFactory = hostWindowFactory;
        }

        public void Initialize(IContentHostViewModel appViewModel, INavigator navigator)
        {
            _mainWindow = _hostWindowFactory.CreateHostWindow(appViewModel);
            _mainWindow.Show();

            navigator.GoToIntro();
        }
    }
}