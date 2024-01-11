using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Exceptions
{
    public class CustomerManagerException : Exception
    {
        public CustomerManagerException(string? message) : base(message)
        {
        }

        public CustomerManagerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        // Override the Message property to include more details
        public override string Message
        {
            get
            {
                // If you have additional information, you can include it here
                if (InnerException is CustomerManagerException innerException)
                {
                    return $"{base.Message} Additional Details: {innerException.Message}";
                }

                return base.Message;
            }
        }
    }
}
