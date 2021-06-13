using System;

namespace Lotos.Exceptions
{
    public class FailConnectLotosException : LotosException
    {
        public FailConnectLotosException(string message) : base(message)
        {

        }
    }
}
