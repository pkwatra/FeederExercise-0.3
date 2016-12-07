using Org.Feeder.App.Framework;
using Org.Feeder.App.Framework.Event;
using Org.Feeder.App.Framework.Startup;
using Prism.Events;

namespace Org.Feeder.App.ViewModels
{
    public class AppShellViewModel : ViewModelBase, IContentHostViewModel
    {
        private IViewModel _content;
        private IEventAggregator _eventAggreator;
        private bool _isLoading;
        public AppShellViewModel(IEventAggregator eventAggregator)
        {
            _eventAggreator = eventAggregator;
            _eventAggreator.GetEvent<ShowLoadingEvent>().Subscribe(ShowLoading);

            IsLoading = false;
        }


        private void ShowLoading(bool value)
        {
            IsLoading = value;
        }

        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Current ViewModel displayed on the application
        /// </summary>
        public IViewModel Content
        {
            get { return _content; }
            set
            {
                if (_content == value)
                    return;

                _content = value;
                OnPropertyChanged();
            }
        }
    }

}
