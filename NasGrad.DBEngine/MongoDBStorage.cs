using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NasGrad.DBEngine
{
    public class MongoDBStorage : IDBStorage
    {
        private readonly IMongoDatabase _database;

        public MongoDBStorage(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<List<NasGradType>> GetConfiguration()
        {
            var dbCollection = _database.GetCollection<NasGradType>(Constants.TypeTableName);
            var result = await dbCollection.Find(FilterDefinition<NasGradType>.Empty).ToListAsync();

            return result;
        }

        public async Task<List<NasGradIssue>> GetIssues()
        {
            var dbCollection = _database.GetCollection<NasGradIssue>(Constants.IssueTableName);
            var result = await dbCollection.Find(issue => issue.IsApproved == true).ToListAsync();

            return result;
        }

        public async Task<List<NasGradIssue>> GetAllIssuesForApproval()
        {
            var dbCollection = _database.GetCollection<NasGradIssue>(Constants.IssueTableName);
            var result = await dbCollection.Find(c => c.IsApproved == false).ToListAsync();

            return result;
        }

        public async Task<NasGradIssue> GetIssue(string issueId)
        {
            var dbCollection = _database.GetCollection<NasGradIssue>(Constants.IssueTableName);
            var result = await dbCollection.Find(i => string.Equals(i.Id, issueId)).ToListAsync();
            if (result.Count == 0 || result.Count > 1)
            {
                return null;
            }

            return result[0];
        }

        public async Task<List<NasGradCategory>> GetCategories()
        {
            var dbCollection = _database.GetCollection<NasGradCategory>(Constants.CategoryTableName);
            var result = await dbCollection.Find(FilterDefinition<NasGradCategory>.Empty).ToListAsync();

            return result;
        }

        public async Task<List<NasGradCategory>> GetSelectedCategories(string[] ids)
        {
            var dbCollection = _database.GetCollection<NasGradCategory>(Constants.CategoryTableName);
            var result = await dbCollection.Find(c => ids.ToList().Contains(c.Id)).ToListAsync();

            return result;
        }

        public async Task<NasGradCategory> GetCategory(string id)
        {
            var dbCollection = _database.GetCollection<NasGradCategory>(Constants.CategoryTableName);
            var result = await dbCollection.Find(c => string.Equals(c.Id, id)).ToListAsync();
            if (result.Count == 0 || result.Count > 1)
            {
                return null;
            }

            return result[0];
        }

        public async Task<List<NasGradPicture>> GetPictures()
        {
            var dbCollection = _database.GetCollection<NasGradPicture>(Constants.PictureTableName);
            var result = await dbCollection.Find(FilterDefinition<NasGradPicture>.Empty).ToListAsync();

            return result;
        }

        public async Task<NasGradPicture> GetPicture(string id)
        {
            var dbCollection = _database.GetCollection<NasGradPicture>(Constants.PictureTableName);
            var result = await dbCollection.Find(c => string.Equals(c.Id, id)).ToListAsync();
            if (result.Count == 0 || result.Count > 1)
            {
                return null;
            }

            return result[0];
        }

        public async Task<bool> SetVisibility(string id, bool visible)
        {
            try
            {
                var updatePictureVisibility = Builders<NasGradPicture>.Update.Set(c => c.Visible, visible);
                var dbCollection = _database.GetCollection<NasGradPicture>(Constants.PictureTableName);
                await dbCollection.UpdateOneAsync(
                    Builders<NasGradPicture>.Filter.Eq(d => d.Id, id),
                    updatePictureVisibility
                    );
                return true;
            }
            catch (System.Exception e)
            {
                throw new MongoDBStorageException($"Error while setting visibility in Pictures collection.", e);
            }
        }

        public async Task InsertPicture(NasGradPicture pic)
        {
            try
            {
                var dbCollection = _database.GetCollection<NasGradPicture>(Constants.PictureTableName);
                await dbCollection.InsertOneAsync(pic);
            }
            catch (System.Exception e)
            {
                throw new MongoDBStorageException($"Error while adding new item in Pictures collection.", e);
            }
        }

        public async Task InsertNewIssue(NasGradIssue issue)
        {
            try
            {
                var dbCollection = _database.GetCollection<NasGradIssue>(Constants.IssueTableName);
                await dbCollection.InsertOneAsync(issue);
            }
            catch (System.Exception e)
            {
                throw new MongoDBStorageException($"Error while adding new item in Issue collection.", e);
            }
        }

        public async Task<bool> UpdateIssueStatus(string id, int statusId)
        {
            try
            {
                var updateIssueStatus = Builders<NasGradIssue>.Update.Set(c => c.State, (StateEnum)statusId);
                var dbCollection = _database.GetCollection<NasGradIssue>(Constants.IssueTableName);
                await dbCollection.UpdateOneAsync(
                    Builders<NasGradIssue>.Filter.Eq(d => d.Id, id),
                    updateIssueStatus
                );
                return true;
            }
            catch (System.Exception e)
            {
                throw new MongoDBStorageException($"Error while updating issue status.", e);
            }
        }

        public async Task<bool> ApproveIssue(string issueId)
        {
            try
            {
                var updateIssueStatus = Builders<NasGradIssue>.Update.Set(c => c.IsApproved, true);
                var dbCollection = _database.GetCollection<NasGradIssue>(Constants.IssueTableName);
                await dbCollection.UpdateOneAsync(
                    Builders<NasGradIssue>.Filter.Eq(d => d.Id, issueId),
                    updateIssueStatus
                );
                return true;
            }
            catch (System.Exception e)
            {
                throw new MongoDBStorageException($"Error while approving issue.", e);
            }
        }

        public async Task<bool> DeleteIssue(string issueId)
        {
            try
            {
                var dbCollection = _database.GetCollection<NasGradIssue>(Constants.IssueTableName);
                await dbCollection.DeleteOneAsync(Builders<NasGradIssue>.Filter.Eq(d => d.Id, issueId));
                return true;
            }
            catch (System.Exception e)
            {
                throw new MongoDBStorageException($"Error while approving issue.", e);
            }
        }

        public async Task<NasGradUser> GetUser(string username)
        {
            var dbCollection = _database.GetCollection<NasGradUser>(Constants.UserTableName);
            var result = await dbCollection.Find(i => string.Equals(i.Username, username)).ToListAsync();
            if (result.Count == 0 || result.Count > 1)
            {
                return null;
            }

            return result[0];
        }

        public async Task<NasGradRole> GetRole(string roleId)
        {
            var dbCollection = _database.GetCollection<NasGradRole>(Constants.RoleTableName);
            var result = await dbCollection.Find(i => string.Equals(i.Id, roleId)).ToListAsync();
            if (result.Count == 0 || result.Count > 1)
            {
                return null;
            }

            return result[0];
        }

        
    }
}
