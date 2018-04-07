using System;
using System.Runtime.Serialization;

namespace ISofA.SL.Exceptions
{
    [Serializable]
    public class TheaterNotFoundException : Exception
    {        
        public TheaterNotFoundException(int theaterId) : base($"Theater with id {theaterId} not found")
        {
        }
    }
}