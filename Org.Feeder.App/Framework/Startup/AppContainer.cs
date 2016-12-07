using Autofac;
using Org.Feeder.App.Framework.Navigate;
using Org.Feeder.Service;
using Org.Feeder.App.ViewModels;
using Prism.Events;

namespace Org.Feeder.App.Framework.Startup
{
    public static class AppContainer
    {
        public static IContainer Container { get; private set; }

        public static IContainer BootStrap()
        {
            var builder = new ContainerBuilder();
         

            builder.RegisterType<AppShellViewModel>()
                .As<IContentHostViewModel>().SingleInstance();

            builder.RegisterType<EventAggregator>()
             .As<IEventAggregator>().SingleInstance();

            builder.RegisterType<Navigator>()
              .As<INavigator>().SingleInstance();

            builder.RegisterType<DataService>()
               .As<IDataService>();

            builder.RegisterType<IntroViewModel>()
             .Named<IViewModel>("IntroVM");

            builder.RegisterType<MainViewModel>()
            .Named<IViewModel>("MainVM");

            builder.RegisterType<CommentViewModel>()
            .Named<IViewModel>("CommentVM");

            Container = builder.Build();

            return Container;
        }
    }
}
