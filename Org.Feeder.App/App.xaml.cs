using System.Windows;
using Org.Feeder.App.Framework;
using Org.Feeder.App.Models;
using Org.Feeder.App.ViewModels;
using Autofac;
namespace Org.Feeder.App
{
    public partial class App
    {        
        private void App_OnStartup(object sender, StartupEventArgs e)
        {           
            var bootstrapper = new Bootstrapper(new HostWindowFactory());
            var continer = bootstrapper.BootStrap();

            var appShellVM = continer.Resolve<IContentHostViewModel>();
            var navigation = continer.Resolve<INavigator>();

            bootstrapper.Initialize(appShellVM, navigation);            
            
        }
    }
}
