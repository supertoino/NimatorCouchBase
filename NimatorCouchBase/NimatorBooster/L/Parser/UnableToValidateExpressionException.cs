using System;

namespace NimatorCouchBase.NimatorBooster.L.Parser
{
    public class UnableToValidateExpressionException : Exception
    {
        public UnableToValidateExpressionException(string pExceptionMessage) : base(pExceptionMessage)
        { }
    }
}