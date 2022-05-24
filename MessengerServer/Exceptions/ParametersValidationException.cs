using System;

namespace MessengerServer.Exceptions
{
    public class ParametersValidationException : Exception
    {
        public ParametersValidationException(string message) : base(message)
        {
        }
    }
}
