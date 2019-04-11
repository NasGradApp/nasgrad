using System;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NasGrad.DBEngine
{
    public class MongoDBStorage : IDBStorage
    {
        private const int MaxLikeCount = 1000;
        private const int MaxDislikeCount = 1000;
        private readonly Type _nasGradCityServiceType = typeof(NasGradCityService);
        private readonly Type _nasGradCityServiceTypeType = typeof(NasGradCityServiceType);
        private readonly Type _nasGradCategoryType = typeof(NasGradCategory);
        private readonly Type _nasGradIssueType = typeof(NasGradIssue);
        private readonly Type _nasGradPictureType = typeof(NasGradPicture);
        private readonly Type _nasGradRegionType = typeof(NasGradRegion);
        private readonly Type _nasGradRoleType = typeof(NasGradRole);
        private readonly Type _nasGradTypeType = typeof(NasGradType);
        private readonly Type _nasGradUserType = typeof(NasGradUser);

        private readonly IMongoDatabase _database;

        public MongoDBStorage(IMongoDatabase database)
        {
            _database = database;
        }

        #region [Generic]

        private string GetTableName<T>()
        {
            var t = typeof(T);

            if (t == _nasGradCityServiceType)
            {
                return Constants.TableName.CityService;
            }
            if (t == _nasGradCityServiceTypeType)
            {
                return Constants.TableName.CityServiceTypes;
            }
            if (t == _nasGradCategoryType)
            {
                return Constants.TableName.Category;
            }
            if (t == _nasGradIssueType)
            {
                return Constants.TableName.Issue;
            }
            if (t == _nasGradPictureType)
            {
                return Constants.TableName.Picture;
            }
            if (t == _nasGradRegionType)
            {
                return Constants.TableName.Region;
            }
            if (t == _nasGradRoleType)
            {
                return Constants.TableName.Role;
            }
            if (t == _nasGradTypeType)
            {
                return Constants.TableName.Type;
            }
            if (t == _nasGradUserType)
            {
                return Constants.TableName.User;
            }

            throw new NotSupportedException($"Object not supported. Type provided: {t}");
        }

        private async Task<bool> InsertGeneric<T>(T itemToInsert)
        {
            string tableName = GetTableName<T>();
            try
            {
                var dbCollection = _database.GetCollection<T>(tableName);
                await dbCollection.InsertOneAsync(itemToInsert);
                return true;
            }
            catch (System.Exception e)
            {
                throw new MongoDBStorageException($"Error while adding new item in {tableName} collection.", e);
            }
        }

       

        private async Task<List<T>> GetAllGeneric<T>(FilterDefinition<T> additionalFilter)
        {
            FilterDefinition<T> effectiveFilter = additionalFilter?? Builders<T>.Filter.Empty;
            var dbCollection = _database.GetCollection<T>(GetTableName<T>());
            return await dbCollection.Find(effectiveFilter).ToListAsync();
        }

        private async Task<bool> DeleteGeneric<T>(string itemId) where T : BaseItem
        {
            string tableName = GetTableName<T>();
            try
            {
                var dbCollection = _database.GetCollection<T>(tableName);
                var res = await dbCollection.DeleteOneAsync(Builders<T>.Filter.Eq(d => d.Id, itemId));
                return (res.IsAcknowledged && res.DeletedCount > 0);
            }
            catch (System.Exception e)
            {
                throw new MongoDBStorageException($"Error while deleting item of {tableName}.", e);
            }
        }

        private async Task<T> GetGeneric<T>(string itemId) where T : BaseItem
        {
            var dbCollection = _database.GetCollection<T>(GetTableName<T>());
            var result = await dbCollection.Find(i => string.Equals(i.Id, itemId)).ToListAsync();
            if (result.Count == 0 || result.Count > 1)
            {
                return null;
            }

            return result[0];
        }

        private async Task<bool> UpdateGeneric<T>(string itemId, UpdateDefinition<T> update) where T : BaseItem
        {
            string tableName = GetTableName<T>();
            try
            {
                
                var dbCollection = _database.GetCollection<T>(tableName);
                var res = await dbCollection.UpdateOneAsync(Builders<T>.Filter.Eq(d => d.Id, itemId), update);
                return res.IsAcknowledged && res.MatchedCount > 0;
            }
            catch (System.Exception e)
            {
                throw new MongoDBStorageException($"Error while updating in {tableName} collection.", e);
            }
        }

        #endregion

        public async Task<List<NasGradType>> GetConfiguration()
        {
            return await GetAllGeneric<NasGradType>(null);            
        }

        public async Task<List<NasGradIssue>> GetIssues()
        {
            return await GetAllGeneric<NasGradIssue>(Builders<NasGradIssue>.Filter.Eq(issue => issue.IsApproved, true));
        }

        public async Task<List<NasGradIssue>> GetAllIssuesForApproval()
        {
            return await GetAllGeneric<NasGradIssue>(Builders<NasGradIssue>.Filter.Eq(issue => issue.IsApproved, false));
        }

        public async Task<NasGradIssue> GetIssue(string issueId)
        {
            return await GetGeneric<NasGradIssue>(issueId);
        }

        public async Task<List<NasGradCategory>> GetCategories()
        {
            return await GetAllGeneric<NasGradCategory>(null);
        }

        public async Task<List<NasGradCategory>> GetSelectedCategories(string[] ids)
        {
            List<FilterDefinition<NasGradCategory>> combinedFilter = new List<FilterDefinition<NasGradCategory>>();
            foreach (var oneId in ids)
            {
                combinedFilter.Add(Builders<NasGradCategory>.Filter.Eq(item => item.Id, oneId));
            }

            return await GetAllGeneric<NasGradCategory>(Builders<NasGradCategory>.Filter.Or(combinedFilter.ToArray()));
        }

        public async Task<NasGradCategory> GetCategory(string id)
        {
            return await GetGeneric<NasGradCategory>(id);
        }

        public async Task<List<NasGradPicture>> GetPictures()
        {
            return await GetAllGeneric<NasGradPicture>(null);
        }

        public async Task<NasGradPicture> GetPicture(string id)
        {
            return await GetGeneric<NasGradPicture>(id);
        }

        public async Task<bool> SetVisibility(string id, bool visible)
        {
            var updatePictureVisibility = Builders<NasGradPicture>.Update.Set(c => c.Visible, visible);
            return await UpdateGeneric<NasGradPicture>(id, updatePictureVisibility);
        }

        public async Task<bool> InsertPicture(NasGradPicture pic)
        {
            return await InsertGeneric<NasGradPicture>(pic);
        }

        public async Task<bool> InsertNewIssue(NasGradIssue issue)
        {
            return await InsertGeneric<NasGradIssue>(issue);
        }

        public async Task<bool> UpdateIssueStatus(string id, int statusId)
        {
            var updateIssueStatus = Builders<NasGradIssue>.Update.Set(c => c.State, (StateEnum) statusId);
            return await UpdateGeneric<NasGradIssue>(id, updateIssueStatus);
        }


        public async Task<bool> UpdateIssueLike(string issueId, int increment)
        {
            var findFilter = Builders<NasGradIssue>.Filter.Where(c => c.Id == issueId && c.LikedCount < MaxLikeCount);
            var found = await _database.GetCollection<NasGradIssue>(Constants.TableName.Issue).FindAsync(findFilter);
            if (found.Any())
            {
                var updateIssueLike = Builders<NasGradIssue>.Update.Inc(c => c.LikedCount, increment);
                return await UpdateGeneric<NasGradIssue>(issueId, updateIssueLike);
            }

            return false;
        }

        public async Task<bool> UpdateIssueDislike(string issueId, int increment)
        {
            var findFilter = Builders<NasGradIssue>.Filter.Where(c => c.Id == issueId && c.DislikedCount < MaxDislikeCount);
            var found = await _database.GetCollection<NasGradIssue>(Constants.TableName.Issue).FindAsync(findFilter);
            if (found.Any())
            {
                var updateIssueDislike = Builders<NasGradIssue>.Update.Inc(c => c.DislikedCount, increment);
                return await UpdateGeneric<NasGradIssue>(issueId, updateIssueDislike);
            }

            return false;
        }

        public async Task<bool> CreateCityService(NasGradCityService data)
        {
            return await InsertGeneric(data);
        }

        public async Task<bool> UpdateCityService(NasGradCityService data)
        {
            return await UpdateGeneric<NasGradCityService>(data.Id, Builders<NasGradCityService>.Update.Combine(new[]
            {
                Builders<NasGradCityService>.Update.Set(a => a.Description, data.Description),
                Builders<NasGradCityService>.Update.Set(a => a.Email, data.Email),
                Builders<NasGradCityService>.Update.Set(a => a.Name, data.Name),
                Builders<NasGradCityService>.Update.Set(a => a.Region, data.Region)
            }));
        }

        public async Task<bool> CreateRegion(NasGradRegion data)
        {
            return await InsertGeneric<NasGradRegion>(data);
        }

        public async Task<bool> UpdateRegion(NasGradRegion data)
        {
            return await UpdateGeneric<NasGradRegion>(data.Id, Builders<NasGradRegion>.Update.Combine(new[]
            {
                Builders<NasGradRegion>.Update.Set(a => a.City, data.City),
            }));
        }

        public async Task<bool> CreateType(NasGradType data)
        {
            return await InsertGeneric<NasGradType>(data);
        }

        public async Task<bool> UpdateType(NasGradType data)
        {
            return await UpdateGeneric<NasGradType>(data.Id, Builders<NasGradType>.Update.Combine(new[]
            {
                Builders<NasGradType>.Update.Set(a => a.Description, data.Description),
                Builders<NasGradType>.Update.Set(a => a.Name, data.Name),
            }));
        }

        public async Task<bool> CreateCityServiceType(NasGradCityServiceType data)
        {
            return await InsertGeneric<NasGradCityServiceType>(data);
        }

        public async Task<bool> UpdateCityServiceType(NasGradCityServiceType data)
        {
            return await UpdateGeneric<NasGradCityServiceType>(data.Id, Builders<NasGradCityServiceType>.Update.Combine(new[]
            {
                Builders<NasGradCityServiceType>.Update.Set(a => a.CityService, data.CityService),
                Builders<NasGradCityServiceType>.Update.Set(a => a.Type, data.Type),
            }));
        }

        public async Task<bool> ApproveIssue(string issueId)
        {
            var updateIssueStatus = Builders<NasGradIssue>.Update.Set(c => c.IsApproved, true);
            return await UpdateGeneric<NasGradIssue>(issueId, updateIssueStatus);
        }

        public async Task<bool> DeleteIssue(string issueId)
        {
            return await DeleteGeneric<NasGradIssue>(issueId);
        }

        public async Task<List<NasGradCityService>> GetAllCityServices()
        {
            return await GetAllGeneric<NasGradCityService>(null);
        }

        public async Task<NasGradCityService> GetCityService(string id)
        {
            return await GetGeneric<NasGradCityService>(id);
        }

        public async Task<bool> DeleteCityService(string id)
        {
            return await DeleteGeneric<NasGradCityService>(id);
        }

        public async Task<List<NasGradRegion>> GetAllRegions()
        {
            return await GetAllGeneric<NasGradRegion>(null);
        }

        public async Task<NasGradRegion> GetRegion(string id)
        {
            return await GetGeneric<NasGradRegion>(id);
        }

        public async Task<bool> DeleteRegion(string id)
        {
            return await DeleteGeneric<NasGradRegion>(id);
        }

        public async Task<List<NasGradType>> GetAllTypes()
        {
            return await GetAllGeneric<NasGradType>(null);
        }

        public async Task<NasGradType> GetNasGradType(string id)
        {
            return await GetGeneric<NasGradType>(id);
        }

        public async Task<bool> DeleteType(string id)
        {
            return await DeleteGeneric<NasGradType>(id);
        }

        public async Task<List<NasGradCityServiceType>> GetAllCityServiceTypes()
        {
            return await GetAllGeneric<NasGradCityServiceType>(null);
        }

        public async Task<NasGradCityServiceType> GetNasGradCityServiceType(string id)
        {
            return await GetGeneric<NasGradCityServiceType>(id);
        }

        public async Task<bool> DeleteCityServiceType(string id)
        {
            return await DeleteGeneric<NasGradCityServiceType>(id);
        }
        

        public async Task<NasGradUser> GetUser(string username)
        {
            var dbCollection = _database.GetCollection<NasGradUser>(Constants.TableName.User);
            var result = await dbCollection.Find(i => string.Equals(i.Username, username)).ToListAsync();
            if (result.Count == 0 || result.Count > 1)
            {
                return null;
            }

            return result[0];
        }

        public async Task<NasGradRole> GetRole(string roleId)
        {
            return await GetGeneric<NasGradRole>(roleId);
        }

        
    }
}
