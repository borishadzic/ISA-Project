using System;
using System.Runtime.Serialization;

namespace ISofA.SL.Exceptions
{
    [Serializable]
    public class StageNotFoundException : Exception
    {        
        public StageNotFoundException(int stageId) : base($"Stage with id {stageId} not found")
        {
        }
    }
}