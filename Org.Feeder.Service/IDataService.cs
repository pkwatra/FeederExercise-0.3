using Org.Feeder.Model;
using Org.Feeder.Model.Service;
using System.Collections;
using System.Collections.Generic;

namespace Org.Feeder.Service
{
    public interface IDataService
    {
        PostSummaryResult GetPostSummary();

        PostCommentResult GetPostComment(int postId);

    }
}
