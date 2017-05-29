using System;
using System.Text;

namespace NimatorCouchBase.NimatorBooster.L.Parser
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