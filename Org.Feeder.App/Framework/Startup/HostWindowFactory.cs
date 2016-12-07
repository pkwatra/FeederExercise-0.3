using Org.Feeder.App.Views;

namespace Org.Feeder.App.Framework.Startup
{
    public class HostWindowFactory
    {
        public virtual IWindow CreateHostWindow(IContentHostViewModel viewModel)
        {
            return new AppWindow { DataContext = viewModel };
        }
    }
}