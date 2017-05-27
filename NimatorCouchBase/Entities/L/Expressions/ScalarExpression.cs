using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NimatorCouchBase.Entities.L.Expressions
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
