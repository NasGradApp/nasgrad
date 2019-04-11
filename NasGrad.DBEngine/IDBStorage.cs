using System.Collections.Generic;
using System.Threading.Tasks;

namespace NasGrad.DBEngine
{
    public interface IDBStorage
    {
        Task<List<NasGradType>> GetConfiguration();
        Task<List<NasGradIssue>> GetIssues();
        Task<List<NasGradIssue>> GetAllIssuesForApproval();
        Task<NasGradIssue> GetIssue(string issueId);
        Task<List<NasGradCategory>> GetCategories();
        Task<List<NasGradCategory>> GetSelectedCategories(string[] ids);
        Task<NasGradCategory> GetCategory(string id);
        Task<List<NasGradPicture>> GetPictures();
        Task<NasGradPicture> GetPicture(string id);
        Task<bool> SetVisibility(string id, bool visible);

        Task<bool> InsertPicture(NasGradPicture pic);
        Task<bool> InsertNewIssue(NasGradIssue issue);
        Task<bool> UpdateIssueStatus(string id, int statusId);
        Task<NasGradUser> GetUser(string username);
        Task<NasGradRole> GetRole(string roleId);
        Task<bool> ApproveIssue(string issueId);
        Task<bool> DeleteIssue(string id);
        Task<List<NasGradCityService>> GetAllCityServices();
        Task<NasGradCityService> GetCityService(string id);
        Task<bool> DeleteCityService(string id);
        Task<List<NasGradRegion>> GetAllRegions();
        Task<NasGradRegion> GetRegion(string id);
        Task<bool> DeleteRegion(string id);
        Task<List<NasGradType>> GetAllTypes();
        Task<NasGradType> GetNasGradType(string id);
        Task<bool> DeleteType(string id);
        Task<List<NasGradCityServiceType>> GetAllCityServiceTypes();
        Task<NasGradCityServiceType> GetNasGradCityServiceType(string id);
        Task<bool> DeleteCityServiceType(string id);
        Task<bool> UpdateIssueLike(string issueId, int increment);
        Task<bool> UpdateIssueDislike(string issueId, int increment);
        Task<bool> CreateCityService(NasGradCityService data);
        Task<bool> UpdateCityService(NasGradCityService data);
        Task<bool> CreateRegion(NasGradRegion data);
        Task<bool> UpdateRegion(NasGradRegion data);
        Task<bool> CreateType(NasGradType data);
        Task<bool> UpdateType(NasGradType data);
        Task<bool> CreateCityServiceType(NasGradCityServiceType data);
        Task<bool> UpdateCityServiceType(NasGradCityServiceType data);
    }
}
