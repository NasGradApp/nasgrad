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
        private readonly string _username;
        private readonly string _password;
        private readonly string _dbName;
        private readonly bool _doInitializeData;
        private readonly bool _doDropDatabase;

        private IMongoDatabase _db;
        private IMongoCollection<NasGradType> _typeCollection;
        private IMongoCollection<NasGradIssue> _issueCollection;
        private IMongoCollection<NasGradCategory> _categoryCollection;
        private IMongoCollection<NasGradPicture> _pictureCollection;
        private IMongoCollection<NasGradRole> _roleCollection;
        private IMongoCollection<NasGradUser> _userCollection;
        private IMongoCollection<NasGradRegion> _regionCollection;
        private IMongoCollection<NasGradCityService> _cityServiceCollection;
        private IMongoCollection<NasGradCityServiceType> _cityServiceTypeCollection;
        private List<Tuple<string, string, AuthRoleType>> _users;


        public MongoDBInitializer(string serverAddress, string serverPort, string username, string password,
            string dbName, List<Tuple<string, string, AuthRoleType>> users, bool doInitializeData, bool doDropDatabase)
        {
            _serverAddress = serverAddress;
            _serverPort = serverPort;
            _username = username;
            _password = password;
            _dbName = dbName;
            _users = users; ;
            _doInitializeData = doInitializeData;
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

        public void InitializeManually()
        {
            try
            {
                CreateDatabase();
                CreateCollections();
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

            _typeCollection = _db.GetCollection<NasGradType>(Constants.TableName.Type);
            _issueCollection = _db.GetCollection<NasGradIssue>(Constants.TableName.Issue);
            _categoryCollection = _db.GetCollection<NasGradCategory>(Constants.TableName.Category);
            _pictureCollection = _db.GetCollection<NasGradPicture>(Constants.TableName.Picture);
            _roleCollection = _db.GetCollection<NasGradRole>(Constants.TableName.Role);
            _userCollection = _db.GetCollection<NasGradUser>(Constants.TableName.User);
            _regionCollection = _db.GetCollection<NasGradRegion>(Constants.TableName.Region);
            _cityServiceCollection = _db.GetCollection<NasGradCityService>(Constants.TableName.CityService);
            _cityServiceTypeCollection = _db.GetCollection<NasGradCityServiceType>(Constants.TableName.CityServiceTypes);
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
                    _typeCollection.InsertMany(initialRecords.Types);
                }

                if (initialRecords.Issues != null && initialRecords.Issues.Count > 0)
                {
                    _issueCollection.InsertMany(initialRecords.Issues);
                }

                if (initialRecords.Pictures != null && initialRecords.Pictures.Count > 0)
                {
                    _pictureCollection.InsertMany(initialRecords.Pictures);
                }

                if (initialRecords.Regions != null && initialRecords.Regions.Count > 0)
                {
                    _regionCollection.InsertMany(initialRecords.Regions);
                }

                if (initialRecords.CityServices != null && initialRecords.CityServices.Count > 0)
                {
                    _cityServiceCollection.InsertMany(initialRecords.CityServices);
                }

                if (initialRecords.CityServiceTypes != null && initialRecords.CityServiceTypes.Count > 0)
                {
                    _cityServiceTypeCollection.InsertMany(initialRecords.CityServiceTypes);
                }

                Console.WriteLine("Done");
            }

            foreach (var user in _users)
            {
                Console.Write("Initializing {0} user...", user.Item3);

                var newRole = new NasGradRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Type = (int) user.Item3,
                    Description = $"User with type of: {user.Item3}"
                };
                _roleCollection.InsertOne(newRole);

                var newUser = new NasGradUser
                {
                    Id = Guid.NewGuid().ToString(),
                    RoleId = newRole.Id,
                    Salt = Guid.NewGuid().ToString(),
                    Username = user.Item1
                };
                newUser.PasswordHash = CryptoUtil.GenerateHash(newUser.Salt + user.Item2);
                _userCollection.InsertOne(newUser);
                Console.WriteLine("Done");
            }
        }

        public string AddNewRegion(string regionCity)
        {
            var newId = Guid.NewGuid().ToString();
            _regionCollection.InsertOne(new NasGradRegion
            {
                Id = newId,
                City = regionCity
            });

            return newId;
        }

        public string AddNewType(string typeName, string typeDescription)
        {
            var newId = Guid.NewGuid().ToString();
            _typeCollection.InsertOne(new NasGradType
            {
                Id = newId,
                Name = typeName,
                Description = typeDescription
            });

            return newId;
        }

        public string AddNewCityService(string cityServiceName, string cityServiceDescription, string cityServiceEmail,
            string cityServiceRegionId)
        {
            var newId = Guid.NewGuid().ToString();
            _cityServiceCollection.InsertOne(new NasGradCityService
            {
                Id = newId,
                Name = cityServiceName,
                Description = cityServiceDescription,
                Email = cityServiceEmail,
                Region = cityServiceRegionId,
            });

            return newId;
        }

        public string AddNewCityServiceType(string cityServiceId, string typeId)
        {
            var newId = Guid.NewGuid().ToString();
            _cityServiceTypeCollection.InsertOne(new NasGradCityServiceType
            {
                Id = newId,
                CityService = cityServiceId,
                Type = typeId
            });

            return newId;
        }
    }

    internal class InitialRecords
    {
        public List<NasGradType> Types { get; set; }
        public List<NasGradIssue> Issues { get; set; }
        public List<NasGradCategory> Categories{ get; set; }
        public List<NasGradPicture> Pictures { get; set; }
        public List<NasGradRegion> Regions { get; set; }
        public List<NasGradCityService> CityServices { get; set; }
        public List<NasGradCityServiceType> CityServiceTypes { get; set; }
    }
}
