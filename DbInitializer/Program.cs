using Microsoft.Extensions.Configuration;
using NasGrad.DBEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DbInitializer
{
    class Program
    {
        private static string _serverAddress;
        private static string _serverPort;
        private static string _dbUsername;
        private static string _dbPassword;
        private static string _dbName;
        private static bool _doInitializeData;
        private static bool _doInitializeAdminUser;
        private static bool _doInitializeReadonlyUser;
        private static List<Tuple<string, string, AuthRoleType>> _users = new List<Tuple<string, string, AuthRoleType>>();
        private static bool _doManualInitialization;
        public static IConfigurationRoot Configuration { get; private set; }


        public static void Main(string[] args)
        {
            try
            {
                var doQuit = ShowMainMenu();
                if (doQuit)
                    return;

                _users.Clear();//always start fresh

                var doDropDatabase = ShowDropDatabaseMenu();
                
                InputDatabaseConnectionDetails();//we need DB connection details always

                if (_doInitializeAdminUser)
                {
                    InputUserDetails(AuthRoleType.Admin);
                }

                if (_doInitializeReadonlyUser)
                {
                    InputUserDetails(AuthRoleType.ReadOnly);
                }

                var initializer = new MongoDBInitializer(
                    _serverAddress,
                    _serverPort,
                    _dbUsername,
                    _dbPassword,
                    _dbName,
                    _users,
                    _doInitializeData,
                    doDropDatabase);

                if (!_doManualInitialization)
                {
                    initializer.Initialize();
                }
                else
                {
                    DoManualInitialization(initializer);
                }

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

        private static void DoManualInitialization(MongoDBInitializer initializer)
        {
            initializer.InitializeManually();
            char key ='q';
            do
            {
                Console.Write("\n------- Manual input of data -------");
                Console.WriteLine("\n1. Input Region data");
                Console.WriteLine("2. Input Type data");
                Console.WriteLine("3. Input CityService data");
                Console.WriteLine("4. Input CityService data");
                Console.WriteLine("Q. Quit\n");
                Console.Write("Your choice: ");

                char[] allowedKeys = {'1', '2', '3', '4', 'q', 'Q'};
                do
                {
                    key = Console.ReadKey(true).KeyChar;
                } while (!allowedKeys.Contains(key));

                Console.WriteLine(key);

                if (key == '1')
                {
                    Console.WriteLine("\n------- Initialize Region -------\n");
                    Console.WriteLine("Enter City: ");
                    var city = Console.ReadLine();
                    var newCityId = initializer.AddNewRegion(city);
                    Console.WriteLine("\nRegion added to DB, ID of new item is: {0}", newCityId);
                }
                else if (key == '2')
                {
                    Console.WriteLine("\n------- Initialize Type -------\n");
                    Console.WriteLine("Enter Name: ");
                    var name = Console.ReadLine();
                    Console.WriteLine("Enter Description: ");
                    var description = Console.ReadLine();
                    var newTypeId = initializer.AddNewType(name, description);
                    Console.WriteLine("\nType added to DB, ID of new item is: {0}", newTypeId);
                }
                else if (key == '3')
                {
                    Console.WriteLine("\n------- Initialize CityService -------\n");
                    Console.WriteLine("Enter Name: ");
                    var name = Console.ReadLine();
                    Console.WriteLine("Enter Description: ");
                    var description = Console.ReadLine();
                    Console.WriteLine("Enter Email: ");
                    var email = Console.ReadLine();
                    Console.WriteLine("Enter related Region.Id: ");
                    var regionId = Console.ReadLine();
                    var newCityServiceId = initializer.AddNewCityService(name, description, email, regionId);
                    Console.WriteLine("\nCityService added to DB, ID of new item is: {0}", newCityServiceId);
                }
                else if (key == '4')
                {
                    Console.WriteLine("\n------- Initialize CityServiceType -------\n");
                    Console.WriteLine("Enter related CityService.Id: ");
                    var cityServiceId = Console.ReadLine();
                    Console.WriteLine("Enter related Type.Id: ");
                    var typeId = Console.ReadLine();
                    var newCityServiceTypeId = initializer.AddNewCityServiceType(cityServiceId, typeId);
                    Console.WriteLine("\nCityServiceType added to DB, ID of new item is: {0}", newCityServiceTypeId);
                }
            } while (char.ToLower(key) != 'q');
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

            var doDropDatabase = (char.ToLower(key) == 'y');
            return doDropDatabase;
        }

        private static bool ShowMainMenu()
        {
            Console.WriteLine("\n------- Initialization Options ---------");
            Console.WriteLine("\n1. Initialize only data");
            Console.WriteLine("2. Initialize only admin user");
            Console.WriteLine("3. Initialize only readonly user");
            Console.WriteLine("4. Initialize ALL");
            Console.WriteLine("5. Manually input data");
            Console.WriteLine("Q. Quit\n");
            Console.Write("Your choice: ");

            char[] allowedKeys = {'1', '2', '3', '4', '5', 'q', 'Q'};
            char key;
            do
            {
                key = Console.ReadKey(true).KeyChar;
            } while (!allowedKeys.Contains(key));

            Console.WriteLine(key);

            _doInitializeData = (key == '1' || key == '4');
            _doInitializeAdminUser = (key == '2' || key == '4');
            _doInitializeReadonlyUser = (key == '3' || key == '4');
            _doManualInitialization = (key == '5');

            var doQuit = (char.ToLower(key) == 'q');
            return doQuit;
        }

        private static void InputUserDetails(AuthRoleType userType)
        {
            Console.WriteLine("Enter {0} user username:", userType);
            var username = Console.ReadLine();
            Console.WriteLine("Enter {0} user password:", userType);
            var password = Console.ReadLine();
            _users.Add(new Tuple<string, string, AuthRoleType>(username, password, userType));
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
    }
}
