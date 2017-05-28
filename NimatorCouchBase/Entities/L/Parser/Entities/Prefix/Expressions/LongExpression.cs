using System;
using System.Text;
using NimatorCouchBase.Entities.L.Parser.Entities.Interfaces;

namespace NimatorCouchBase.Entities.L.Parser.Entities.Prefix.Expressions
{
    public class LongExpression : IExpression
    {
        public LongExpression(object pValue)
        {
            Value = pValue;
        }

        public object Value { get; }

        public void Print(StringBuilder pBuilder)
        {
            pBuilder.Append(Convert.ToString(Value));
        }
    }
}
