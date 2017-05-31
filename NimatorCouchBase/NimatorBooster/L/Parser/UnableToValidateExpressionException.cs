using System;

public class UnableToValidateExpressionException : Exception
{
    public UnableToValidateExpressionException(string pExceptionMessage) : base(pExceptionMessage)
    { }
}