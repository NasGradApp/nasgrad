using System.Collections.Generic;

namespace NasGrad.DBEngine
{
    public class NasGradIssue
    {
        public string Id { get; set; }
        public string OwnerId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string IssueType { get; set; }
        public StateEnum State { get; set; }
        public List<string> Pictures { get; set; }
        public List<string> Categories { get; set; }
        public IssueLocation Location { get; set; }
        public string PicturePreview { get; set; }
        public int SubmittedCount { get; set; }
    }

    public enum StateEnum
    {
        Submitted = 1,
        Reported = 2,
        Done = 3
    }
}
