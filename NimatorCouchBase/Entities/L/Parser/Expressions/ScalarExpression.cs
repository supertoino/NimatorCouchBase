using System;
using System.Text;

namespace NimatorCouchBase.Entities.L.Parser.Expressions
{
    public class ScalarExpression : IExpression
    {
        public ScalarExpression(object pValue)
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
