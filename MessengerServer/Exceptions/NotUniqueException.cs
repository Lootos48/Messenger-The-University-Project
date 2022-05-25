using System;

namespace MessengerServer.Exceptions
{
    public class NotUniqueException : Exception
    {
        public NotUniqueException(string message) : base(message)
        {
        }
    }
}
