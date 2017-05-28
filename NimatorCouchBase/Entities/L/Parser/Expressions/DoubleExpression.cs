using System;
using System.Text;
using NimatorCouchBase.Entities.L.Parser.Expressions.Interfaces;

namespace NimatorCouchBase.Entities.L.Parser.Expressions
{
    public class DoubleExpression : IExpression
    {
        public DoubleExpression(object pValue)
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