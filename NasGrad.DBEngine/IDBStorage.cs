using System.Collections.Generic;
using System.Threading.Tasks;

namespace NasGrad.DBEngine
{
    public interface IDBStorage
    {
        Task<List<NasGradType>> GetConfiguration();
        Task<List<NasGradIssueWrapper>> GetIssues();
        Task<NasGradIssueWrapper> GetIssue(string issueId);
    }
}
