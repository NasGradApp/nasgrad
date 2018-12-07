using System;

namespace NasGrad.DBEngine
{
    public class MongoDBStorageException: Exception
    {
        public MongoDBStorageException()
        {
        }

        public MongoDBStorageException(string message) : base(message)
        {
        }

        public MongoDBStorageException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
