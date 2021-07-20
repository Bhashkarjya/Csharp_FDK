using System;

namespace FDK.Exceptions
{
    public class InvalidEnvironmentException : Exception
    {
        public InvalidEnvironmentException(string message) : base(message) {}
    }
}