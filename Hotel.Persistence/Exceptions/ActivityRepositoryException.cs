using System;

namespace Hotel.Persistence.Exceptions
{
    internal class ActivityRepositoryException : Exception
    {
        public ActivityRepositoryException()
        {
        }

        public ActivityRepositoryException(string message)
            : base(message)
        {
        }

        public ActivityRepositoryException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
