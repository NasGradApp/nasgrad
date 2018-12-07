using System.Collections.Generic;

namespace NasGrad.DBEngine
{
    public class NasGradIssueWrapper
    {
        public string Id { get; set; }
        public int Count { get; set; }
        public List<NasGradIssue> Issues { get; set; }
    }
}
