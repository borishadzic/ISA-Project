using System;
using System.Runtime.Serialization;

namespace ISofA.SL.Exceptions
{
    [Serializable]
    public class BadRequestException : Exception
    {        
        public BadRequestException(string message) : base(message)
        {
        }
    }
}