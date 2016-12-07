using Microsoft.Expression.Interactivity.Core;
using Org.Feeder.App.Framework.Event;
using Org.Feeder.App.Framework.Navigate;
using Org.Feeder.Model;
using Org.Feeder.Service;
using Prism.Events;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Org.Feeder.App.ViewModels
{
    public class CommentViewModel : ViewModelBase
    {
        private IDataService _dataService;     
        private INavigator _navigator;
        private PostSummary _postSummary;

        private Post _post;
        private List<Comment> _comments;
        private FeederUser _user;
        private IEventAggregator _eventAggregator;

        public CommentViewModel(INavigator navigator,
                                IDataService dataService,
                                IEventAggregator eventAggregator,       
                                PostSummary postSummary)
        {
            _navigator = navigator;
            _dataService = dataService;          
            _postSummary = postSummary;
            _eventAggregator = eventAggregator;

            LoadedCommand = new ActionCommand(OnLoaded);
            GoBackCommand = new ActionCommand(OnGoBack);

        }

        private void OnGoBack()
        {
            _navigator.GoToMain();
        }

        public Post Post
        {
            get
            {
                return _post;
            }
            set
            {
                _post = value;
                OnPropertyChanged();
            }
        }
        public FeederUser User
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
                OnPropertyChanged();
            }
        }
        public List<Comment> Comments
        {
            get
            {
                return _comments;
            }
            set
            {
                _comments = value;
                OnPropertyChanged();
            }
        }

        public ActionCommand LoadedCommand { get; private set; }
        public ActionCommand GoBackCommand { get; private set; }

        public void OnLoaded()
        {            
            //Show Loading
            _eventAggregator.GetEvent<ShowLoadingEvent>().Publish(true);

            GetPostComments();
        }

        //Will change to Task
        public Task GetPostComments()
        {
            return Task.Factory.StartNew(() =>
            {
                var postCommentResult = _dataService.GetPostComment(_postSummary.PostId);

                if (!string.IsNullOrEmpty(postCommentResult.Error))
                {
                    _navigator.ShowError(postCommentResult.ErrorType, postCommentResult.Error, () => { GoToMain(); });
                }
                else
                {
                    if (postCommentResult.Comments == null || postCommentResult.Post == null)
                        return;

                    Post = postCommentResult.Post;
                    Comments = postCommentResult.Comments;
                    User = postCommentResult.User;
                }

                _eventAggregator.GetEvent<ShowLoadingEvent>().Publish(false);
            });
        }

        private void GoToMain()
        {
            _navigator.GoToMain();
        }

    }
}
