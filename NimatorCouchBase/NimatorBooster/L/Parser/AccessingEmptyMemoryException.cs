using System;

namespace NimatorCouchBase.NimatorBooster.L.Parser
{
    public class AccessingEmptyMemoryException : Exception
    {
        public AccessingEmptyMemoryException(string pExceptionMessage) : base(pExceptionMessage)
        {
            
        }
    }
}