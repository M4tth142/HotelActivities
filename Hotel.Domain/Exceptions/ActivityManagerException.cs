using System;

namespace Hotel.Domain.Exceptions
{
    internal class ActivityManagerException : Exception
    {
        public ActivityManagerException()
        {
        }

        public ActivityManagerException(string message)
            : base(message)
        {
        }

        public ActivityManagerException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
