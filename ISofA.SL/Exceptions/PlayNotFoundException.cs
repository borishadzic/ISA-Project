using System;
using System.Runtime.Serialization;

namespace ISofA.SL.Exceptions
{
    [Serializable]
    public class PlayNotFoundException : Exception
    {        
        public PlayNotFoundException(int playId) : base($"Play with id {playId} not found")
        {
        }
    }
}