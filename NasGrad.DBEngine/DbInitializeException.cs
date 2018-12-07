using System;
using System.Runtime.Serialization;

namespace NasGrad.DBEngine
{
    public class DbInitializeException : Exception
    {
        public DbInitializeException()
        {
        }

        public DbInitializeException(string message) : base(message)
        {
        }

        public DbInitializeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DbInitializeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }}
