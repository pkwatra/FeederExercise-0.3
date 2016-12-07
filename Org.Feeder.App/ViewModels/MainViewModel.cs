using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Org.Feeder.App.Framework;
using Org.Feeder.App.Models;

namespace Org.Feeder.App.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly List<PostSummary> _initialPosts;
        private readonly INavigator _navigator;

        public MainViewModel(INavigator navigator)
        {
            _initialPosts = new List<PostSummary>();
            _navigator = navigator;
            Posts = new ObservableCollection<PostSummary>(_initialPosts);

            FilterCommand = new ParametrizedCommand<string>(Filter);
            SelectCommand = new ParametrizedCommand<PostSummary>(PostSelected);
        }

        public ObservableCollection<PostSummary> Posts { get; private set; }
        public ParametrizedCommand<string> FilterCommand { get; private set; }
        public ParametrizedCommand<PostSummary> SelectCommand { get; private set; }

        private void Filter(string filter)
        {
            Posts.Clear();
            var filteredPosts =
                    _initialPosts.Where(x => x.Title.ToLower().Contains(filter.ToLower()));

            foreach (var post in filteredPosts)
            {
                Posts.Add(post);
            }
        }

        private void PostSelected(PostSummary postSummary)
        {
            _navigator.GoToComment();
        }
    }
}
