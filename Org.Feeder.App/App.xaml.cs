using System.Windows;
using Org.Feeder.App.Framework.Startup;
using Autofac;
using Org.Feeder.App.Framework.Navigate;

namespace Org.Feeder.App
{
    public partial class App
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var bootstrapper = new Bootstrapper(new HostWindowFactory());

            var container = AppContainer.BootStrap();

            var appShellVM = container.Resolve<IContentHostViewModel>();
            var navigation = container.Resolve<INavigator>();

            bootstrapper.Initialize(appShellVM, navigation);
        }
    }
}
