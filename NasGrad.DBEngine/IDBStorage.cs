using System.Collections.Generic;
using System.Threading.Tasks;

namespace NasGrad.DBEngine
{
    public interface IDBStorage
    {
        Task<List<NasGradType>> GetConfiguration();
        Task<List<NasGradIssue>> GetIssues();
        Task<NasGradIssue> GetIssue(string issueId);
        Task<List<NasGradCategory>> GetCategories();
        Task<List<NasGradCategory>> GetSelectedCategories(string[] ids);
        Task<NasGradCategory> GetCategory(string id);
    }
}
