using System;

namespace NimatorCouchBase.NimatorBooster.L.Parser
{
    public class UnableToParseLTokenTypeException : Exception
    {
        public UnableToParseLTokenTypeException(string pExceptionMessage) : base(pExceptionMessage)
        {

        }
    }
}