using System;
using System.Runtime.Serialization;

namespace ISofA.SL.Exceptions
{
    [Serializable]
    public class UnauthorizedRequestException : Exception
    {        
        public UnauthorizedRequestException() : base("Unauthorized Request")
        {
        }
    }
}