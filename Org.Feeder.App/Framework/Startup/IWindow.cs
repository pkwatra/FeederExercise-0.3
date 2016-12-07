namespace Org.Feeder.App.Framework.Startup
{
    public interface IWindow
    {
        object DataContext { get; set; }
        void Show();
    }
}