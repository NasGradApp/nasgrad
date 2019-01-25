﻿using MongoDB.Driver;
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
        private readonly string _username;
        private readonly string _password;
        private readonly string _dbName;
        private readonly string _adminUsername;
        private readonly string _adminPassword;
        private readonly bool _doInitializeData;
        private readonly bool _doInitializeAdminUser;
        private readonly bool _doDropDatabase;

        private IMongoDatabase _db;
        private IMongoCollection<NasGradType> _configCollection;
        private IMongoCollection<NasGradIssue> _issueCollection;
        private IMongoCollection<NasGradCategory> _categoryCollection;
        private IMongoCollection<NasGradPicture> _pictureCollection;
        private IMongoCollection<NasGradRole> _roleCollection;
        private IMongoCollection<NasGradUser> _userCollection;


        public MongoDBInitializer(string serverAddress, string serverPort, string username, string password,
            string dbName, string adminUsername, string adminPassword, bool doInitializeData,
            bool doInitializeAdminUser, bool doDropDatabase)
        {
            _serverAddress = serverAddress;
            _serverPort = serverPort;
            _username = username;
            _password = password;
            _dbName = dbName;
            _adminUsername = adminUsername;
            _adminPassword = adminPassword;
            _doInitializeData = doInitializeData;
            _doInitializeAdminUser = doInitializeAdminUser;
            _doDropDatabase = doDropDatabase;
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

            var client = MongoDBUtil.CreateMongoClient(_serverAddress, _serverPort, _username, _password);
            if (_doDropDatabase)
            {
                client.DropDatabase(_dbName);
            }

            _db = client.GetDatabase(_dbName);
        }

        private void CreateCollections()
        {
            Console.WriteLine("Create collections");

            _configCollection = _db.GetCollection<NasGradType>(Constants.TypeTableName);
            _issueCollection = _db.GetCollection<NasGradIssue>(Constants.IssueTableName);
            _categoryCollection = _db.GetCollection<NasGradCategory>(Constants.CategoryTableName);
            _pictureCollection = _db.GetCollection<NasGradPicture>(Constants.PictureTableName);
            _roleCollection = _db.GetCollection<NasGradRole>(Constants.RoleTableName);
            _userCollection = _db.GetCollection<NasGradUser>(Constants.UserTableName);
        }

        private void Seed()
        {
            Console.WriteLine("Seed database");

            if (_doInitializeData)
            {
                Console.Write("\nInitializing data...");

                // Read initial records from json
                var initialRecordsFilePath = Path.Combine(
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    "InitialRecords.json"
                );
                var initialRecords = JsonConvert.DeserializeObject<InitialRecords>(
                    File.ReadAllText(initialRecordsFilePath));

                initialRecords.Categories.ForEach(c =>
                {
                    if (string.IsNullOrEmpty(c.Id))
                    {
                        c.Id = Guid.NewGuid().ToString();
                    }
                });

                initialRecords.Types.ForEach(t =>
                {
                    if (string.IsNullOrEmpty(t.Id))
                        t.Id = Guid.NewGuid().ToString();
                });

                initialRecords.Issues.ForEach(i =>
                {
                    if (string.IsNullOrEmpty(i.Id))
                    {
                        i.Id = Guid.NewGuid().ToString();
                    }
                });

                initialRecords.Pictures.ForEach(p =>
                {
                    if (string.IsNullOrEmpty(p.Id))
                    {
                        p.Id = Guid.NewGuid().ToString();
                    }
                });

                if (initialRecords.Categories != null && initialRecords.Categories.Count > 0)
                {
                    _categoryCollection.InsertMany(initialRecords.Categories);
                }

                if (initialRecords.Types != null && initialRecords.Types.Count > 0)
                {
                    _configCollection.InsertMany(initialRecords.Types);
                }

                if (initialRecords.Issues != null && initialRecords.Issues.Count > 0)
                {
                    _issueCollection.InsertMany(initialRecords.Issues);
                }

                if (initialRecords.Pictures != null && initialRecords.Pictures.Count > 0)
                {
                    _pictureCollection.InsertMany(initialRecords.Pictures);
                }
                Console.WriteLine("Done");
            }

            if (_doInitializeAdminUser)
            {
                Console.Write("Initializing admin user...");

                var newRole = new NasGradRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Type = (int) AuthRoleType.Admin,
                    Description = "Administrator of the system"
                };
                _roleCollection.InsertOne(newRole);

                var newUser = new NasGradUser
                {
                    Id = Guid.NewGuid().ToString(),
                    RoleId = newRole.Id,
                    Salt = Guid.NewGuid().ToString(),
                    Username = _adminUsername
                };
                newUser.PasswordHash = CryptoUtil.GenerateHash(newUser.Salt + _adminPassword);
                _userCollection.InsertOne(newUser);
                Console.WriteLine("Done");
            }
        }
    }

    internal class InitialRecords
    {
        public List<NasGradType> Types { get; set; }
        public List<NasGradIssue> Issues { get; set; }
        public List<NasGradCategory> Categories{ get; set; }
        public List<NasGradPicture> Pictures { get; set; }
    }
}
