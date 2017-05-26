using System;

namespace NimatorCouchBase
{
    internal class ErrorWebRequest : Exception
    {
        public ErrorWebRequest(string pMessage) : base(pMessage)
        {
        }
    }
}