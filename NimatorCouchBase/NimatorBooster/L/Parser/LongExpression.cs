using System;
using System.Text;

namespace NimatorCouchBase.NimatorBooster.L.Parser
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
