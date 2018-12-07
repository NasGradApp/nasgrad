using MongoDB.Driver;
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

        public Task<NasGradConfiguration> GetConfiguration()
        {
            var dbCollection = _database.GetCollection<NasGradConfiguration>(Constants.ConfigurationTableName);
            var result = dbCollection.Find(FilterDefinition<NasGradConfiguration>.Empty).ToList();

            return Task.FromResult(result.First());
        }
    }
}
