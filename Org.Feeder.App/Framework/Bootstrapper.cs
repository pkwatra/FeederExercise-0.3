using Autofac;
using Org.Feeder.App.ViewModels;
using Org.Feeder.FeederDb;

namespace Org.Feeder.App.Framework
{
    public class Bootstrapper
    {
        private readonly HostWindowFactory _hostWindowFactory;
        private IWindow _mainWindow;     

        public Bootstrapper(HostWindowFactory hostWindowFactory)
        {
            _hostWindowFactory = hostWindowFactory;
        }

        public IContainer BootStrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<AppShellViewModel>()
                .As<IContentHostViewModel>().SingleInstance();

            builder.RegisterType<Navigator>()
              .As<INavigator>().SingleInstance();

            return builder.Build();
        }

        public void Initialize(IContentHostViewModel appViewModel, INavigator navigator)
        {
            _mainWindow = _hostWindowFactory.CreateHostWindow(appViewModel);
            _mainWindow.Show();

            navigator.GoToIntro();
        }
    }
}