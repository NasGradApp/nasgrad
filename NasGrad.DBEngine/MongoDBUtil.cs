using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace NasGrad.DBEngine
{
    public class MongoDBUtil
    {      
        public static MongoClient CreateMongoClient(string serverAddress, string serverPort, string username, string password)
        {           
            // Database settings
            var settings = new MongoClientSettings
            {
                Server = new MongoServerAddress(
                    serverAddress,
                    Convert.ToInt32(serverPort))
            };
            settings.Credential = MongoCredential.CreateCredential("admin", username, password);

            return new MongoClient(settings);            
        }
    }
}
