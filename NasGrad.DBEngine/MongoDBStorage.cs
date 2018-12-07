﻿using MongoDB.Driver;
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
            var result = await dbCollection.Find(FilterDefinition<NasGradIssue>.Empty).ToListAsync();

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
    }
}