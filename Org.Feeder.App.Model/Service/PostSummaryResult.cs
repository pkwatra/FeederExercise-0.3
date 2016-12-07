using Org.Feeder.Model;
using System.Collections.Generic;

namespace Org.Feeder.Service
{
    public class PostSummaryResult
    {
        public List<PostSummary> PostSummary { get; set; }
        public string Error { get; set; }
        public string ErrorType { get; set; }

    }
}
