using System.Text;
using NimatorCouchBase.NimatorBooster.L.Tokens;

namespace NimatorCouchBase.NimatorBooster.L.Parser
{
    public class PrefixExpression : IExpression
    {
        public PrefixExpression(LTokenType pOperator, IExpression pRight)
        {
            Operator = pOperator;
            Right = pRight;
        }

        public object Value { get; }

        public void Print(StringBuilder pBuilder)
        {
            pBuilder.Append("(").Append(Operator.GetFunctionSyntax());
            Right.Print(pBuilder);
            pBuilder.Append(")");
        }

        private readonly LTokenType Operator;
        private readonly IExpression Right;        
    }
}