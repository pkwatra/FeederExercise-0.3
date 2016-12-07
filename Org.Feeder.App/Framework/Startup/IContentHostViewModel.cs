using Org.Feeder.App.ViewModels;

namespace Org.Feeder.App.Framework.Startup
{
    public interface IContentHostViewModel : IViewModel
    {
        IViewModel Content { get; set; }
        bool IsLoading { get; set; }
    }
}