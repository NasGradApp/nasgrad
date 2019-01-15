using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Reflection;
using NasGrad.DBEngine;

namespace DbInitializer
{
    class Program
    {
        public static IConfigurationRoot Configuration { get; private set; }

        private const string ConfigFileName = "dbInitializerConfig.json";

        public static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("DB initialization started\n");

                Console.WriteLine("\nEnter server address:");
		string serverAddress = Console.ReadLine();
                Console.WriteLine("Enter server port:");
		string serverPort = Console.ReadLine();
                Console.WriteLine("Enter DB username:");
		string dbUsername = Console.ReadLine();
                Console.WriteLine("Enter DB password:");
		string dbPassword = Console.ReadLine();
                Console.WriteLine("Enter DBName:");
		string dbName = Console.ReadLine();

                //InitializeConfiguration();
                //ValidateConfiguration();

                var initializer = new MongoDBInitializer(
                   serverAddress,
                   serverPort,
                   dbUsername,
                   dbPassword,
                   dbName);

                initializer.Initialize();

                Console.WriteLine("\nDB initialized, press any key to exit.");
                Console.ReadKey();
            }
            catch (DbInitializeException e)
            {
                Console.WriteLine(e);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception during db initialization. " + e);
            }
        }

        private static void InitializeConfiguration()
        {
            var fullFilePath = Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), ConfigFileName
            );

            if (!File.Exists(fullFilePath))
                throw new FileNotFoundException($"{ConfigFileName} not found");

            Configuration = new ConfigurationBuilder()
                .AddJsonFile(fullFilePath, true, true)
                .Build();
        }

        private static void ValidateConfiguration()
        {
            if (string.IsNullOrEmpty(Configuration["serverAddress"]))
                throw new ArgumentException($"Server address should be defined in {ConfigFileName}");

            if (string.IsNullOrEmpty(Configuration["serverPort"]))
                throw new ArgumentException($"Server port should be defined in {ConfigFileName}");

            if (!int.TryParse(Configuration["serverPort"], out int result))
                throw new ArgumentException("Server port should be int");

            if (string.IsNullOrEmpty(Configuration["DbName"]))
                throw new ArgumentException($"Database name should be defined in {ConfigFileName}");
        }
    }
}
