using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Org.Feeder.Model;
using Org.Feeder.App.Framework.Command;
using Org.Feeder.App.Framework.Navigate;
using Org.Feeder.Service;
using System.Threading.Tasks;
using Prism.Events;
using Org.Feeder.App.Framework.Event;

namespace Org.Feeder.App.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly INavigator _navigator;
        private readonly IDataService _dataService;        
        private ObservableCollection<PostSummary> _posts;
        private IEventAggregator _eventAggregator;

        public MainViewModel(INavigator navigator, IDataService dataService, IEventAggregator eventAggregator)
        {
            _navigator = navigator;
            _dataService = dataService;
            _eventAggregator = eventAggregator;

            FilterCommand = new ParametrizedCommand<string>(Filter);
            SelectCommand = new ParametrizedCommand<PostSummary>(PostSelected);
            LoadedCommand = new ActionCommand(OnLoaded);

            InitialPosts = new List<PostSummary>();
            Posts = new ObservableCollection<PostSummary>();

        }
    
        public ActionCommand LoadedCommand { get; private set; }
        public ParametrizedCommand<string> FilterCommand { get; private set; }
        public ParametrizedCommand<PostSummary> SelectCommand { get; private set; }

        public List<PostSummary> InitialPosts { get; set; }
        public ObservableCollection<PostSummary> Posts
        {
            get
            {
                return _posts;
            }
            set
            {
                _posts = value;
                OnPropertyChanged();
            }
        }

        private void Filter(string filter)
        {
            Posts.Clear();
            var filteredPosts =
                    InitialPosts.Where(x => x.Title.ToLower().Contains(filter.ToLower()));

            foreach (var post in filteredPosts)
            {
                Posts.Add(post);
            }
        }

        //Data Should be load on page load
        public void OnLoaded()
        {
            ShowLoader();

            GetPostRecords();
        }

        public Task GetPostRecords()
        {
            return Task.Factory.StartNew(() =>
            {
                //Show Loading          
                var postSummaryResult = _dataService.GetPostSummary();

                if (postSummaryResult != null)
                {
                    if (!string.IsNullOrEmpty(postSummaryResult.Error))
                    {
                        _navigator.ShowError(postSummaryResult.ErrorType, postSummaryResult.Error, () => { _navigator.GoToIntro(); });
                    }
                    else
                    {
                        InitialPosts = postSummaryResult.PostSummary;
                        if (InitialPosts != null)
                        {
                            Posts = new ObservableCollection<PostSummary>(InitialPosts);
                        }
                    }                    
                }

                HideLoader();
            });
        }

        public void ShowLoader()
        {
            _eventAggregator.GetEvent<ShowLoadingEvent>().Publish(true);
        }

        public void HideLoader()
        {
            _eventAggregator.GetEvent<ShowLoadingEvent>().Publish(false);
        }


        //Navigate to comment screen
        public void PostSelected(PostSummary postSummary)
        {
            _navigator.GoToComment(postSummary);
        }
    }
}
