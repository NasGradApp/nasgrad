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

        public Task<List<NasGradType>> GetConfiguration()
        {
            var dbCollection = _database.GetCollection<NasGradType>(Constants.TypeTableName);
            var result = dbCollection.Find(FilterDefinition<NasGradType>.Empty).ToList();

            return Task.FromResult(result);
        }
    }
}
