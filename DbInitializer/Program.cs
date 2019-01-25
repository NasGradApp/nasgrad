using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using NasGrad.DBEngine;

namespace DbInitializer
{
    class Program
    {
        private static string _serverAddress;
        private static string _serverPort;
        private static string _dbUsername;
        private static string _dbPassword;
        private static string _dbName;
        private static string _adminUsername;
        private static string _adminPassword;
        private static bool _doInitializeData;
        private static bool _doInitializeAdminUser;
        public static IConfigurationRoot Configuration { get; private set; }

        private const string ConfigFileName = "dbInitializerConfig.json";

        public static void Main(string[] args)
        {
            try
            {
                var doQuit = ShowMainMenu();
                if (doQuit)
                    return;

                var doDropDatabase = ShowDropDatabaseMenu();
                
                InputDatabaseConnectionDetails();//we need DB connection details always

                if (_doInitializeAdminUser)
                {
                    InputAdminUserDetails();
                }

                var initializer = new MongoDBInitializer(
                    _serverAddress,
                    _serverPort,
                    _dbUsername,
                    _dbPassword,
                    _dbName,
                    _adminUsername,
                    _adminPassword,
                    _doInitializeData,
                    _doInitializeAdminUser,
                    doDropDatabase);

                initializer.Initialize();

                Console.WriteLine("\nDB initialized, press any key to exit...");
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

        private static bool ShowDropDatabaseMenu()
        {
            Console.Write("\nDrop existing database [Y/N]: ");

            char[] allowedKeys = {'y', 'Y', 'n', 'N'};
            char key;
            do
            {
                key = Console.ReadKey(true).KeyChar;
            } while (!allowedKeys.Contains(key));

            Console.WriteLine(key);

            var doDropDatabase = (key == 'y' || key == 'Y');
            return doDropDatabase;
        }

        private static bool ShowMainMenu()
        {
            Console.WriteLine("\n------- Initialization Options ---------");
            Console.WriteLine("\n1. Initialize only data");
            Console.WriteLine("2. Initialize only admin user");
            Console.WriteLine("3. Initialize ALL");
            Console.WriteLine("Q. Quit\n");
            Console.Write("Your choice: ");

            char[] allowedKeys = {'1', '2', '3', 'q', 'Q'};
            char key;
            do
            {
                key = Console.ReadKey(true).KeyChar;
            } while (!allowedKeys.Contains(key));

            Console.WriteLine(key);

            _doInitializeData = (key == '1' || key == '3');
            _doInitializeAdminUser = (key == '2' || key == '3');

            var doQuit = (key == 'q' || key == 'Q');
            return doQuit;
        }

        private static void InputAdminUserDetails()
        {
            Console.WriteLine("Enter admin username:");
            _adminUsername = Console.ReadLine();
            Console.WriteLine("Enter admin password:");
            _adminPassword = Console.ReadLine();
        }

        private static void InputDatabaseConnectionDetails()
        {
            Console.WriteLine("DB initialization started\n");

            Console.WriteLine("\nEnter server address:");
            _serverAddress = Console.ReadLine();
            Console.WriteLine("Enter server port:");
            _serverPort = Console.ReadLine();
            Console.WriteLine("Enter DB username:");
            _dbUsername = Console.ReadLine();
            Console.WriteLine("Enter DB password:");
            _dbPassword = Console.ReadLine();
            Console.WriteLine("Enter DBName:");
            _dbName = Console.ReadLine();
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
