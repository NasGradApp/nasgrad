using System.Collections.Generic;

namespace NasGrad.DBEngine
{
    public class NasGradIssue: BaseItem
    {        
        public string OwnerId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> CityServiceTypes { get; set; }
        public StateEnum State { get; set; }
        public List<string> Pictures { get; set; }
        public IssueLocation Location { get; set; }
        public string PicturePreview { get; set; }
        public int SentCount { get; set; }
        public int LikedCount { get; set; }
        public int DislikedCount { get; set; }
        public bool IsApproved { get; set; }
    }

    public enum StateEnum
    {
        Submitted = 1,
        Reported = 2,
        Done = 3
    }
}
