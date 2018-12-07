using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace NasGrad.DBEngine
{
    public class MongoDBInitializer
    {
        private readonly string _serverAddress;
        private readonly string _serverPort;
        private readonly string _dbName;

        private IMongoDatabase _db;
        private IMongoCollection<NasGradType> _configCollection;

        public MongoDBInitializer(string serverAddress, string serverPort, string dbName)
        {
            _serverAddress = serverAddress;
            _serverPort = serverPort;
            _dbName = dbName;
        }

        public void Initialize()
        {
            try
            {
                CreateDatabase();
                CreateCollections();
                Seed();
            }
            catch (Exception e)
            {
                throw new DbInitializeException("Failed to initialize database.", e);
            }
        }

        private void CreateDatabase()
        {
            Console.WriteLine("Create database");

            // Database settings
            var settings = new MongoClientSettings
            {
                Server = new MongoServerAddress(
                    _serverAddress,
                    Convert.ToInt32(_serverPort))
            };

            var client = new MongoClient(settings);
            client.DropDatabase(_dbName);
            _db = client.GetDatabase(_dbName);
        }

        private void CreateCollections()
        {
            Console.WriteLine("Create collections");

            _configCollection = _db.GetCollection<NasGradType>(Constants.TypeTableName);
        }

        private void Seed()
        {
            Console.WriteLine("Seed database");

            // Read initial records from json
            var initialRecordsFilePath = Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "InitialRecords.json"
            );
            var initialRecords = JsonConvert.DeserializeObject<InitialRecords>(
                File.ReadAllText(initialRecordsFilePath));

            initialRecords.Types.ForEach(t =>
            {
                if (string.IsNullOrEmpty(t.Id))
                    t.Id = Guid.NewGuid().ToString();

                t.Categories.ForEach(c =>
                    {
                        if (string.IsNullOrEmpty(c.Id))
                        {
                            c.Id = Guid.NewGuid().ToString();
                        }
                    }
                    );
            });


            if (initialRecords.Types != null)
            {
                _configCollection.InsertMany(initialRecords.Types);
            }

        }
    }

    internal class InitialRecords
    {
        public List<NasGradType> Types { get; set; }
    }
}
