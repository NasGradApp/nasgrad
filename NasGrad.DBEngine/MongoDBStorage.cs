using MongoDB.Driver;
using System.Collections.Generic;
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

        public async Task<List<NasGradIssueWrapper>> GetIssues()
        {
            var dbCollection = _database.GetCollection<NasGradIssueWrapper>(Constants.IssueWrapperTableName);
            var result = await dbCollection.Find(FilterDefinition<NasGradIssueWrapper>.Empty).ToListAsync();

            return result;
        }

        public async Task<NasGradIssueWrapper> GetIssue(string issueId)
        {
            var dbCollection = _database.GetCollection<NasGradIssueWrapper>(Constants.IssueWrapperTableName);
            var result = await dbCollection.Find(i => string.Equals(i.Id, issueId)).ToListAsync();
            if (result.Count == 0 || result.Count > 1)
            {
                return null;
            }

            return result[0];
        }
    }
}
