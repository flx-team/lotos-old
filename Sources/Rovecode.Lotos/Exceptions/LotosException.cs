using System;

namespace Lotos.Exceptions
{
    public class LotosException : Exception
    {
        public LotosException(string message) : base(message)
        {

        }

        public LotosException(string? message, Exception? innerException)
            : base(message, innerException)
        {

        }
    }
}
