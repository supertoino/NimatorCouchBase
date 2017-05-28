using System.Text;
using NimatorCouchBase.Entities.L.Tokens;

namespace NimatorCouchBase.Entities.L.Parser
{
    public class PrefixExpression : IExpression
    {
        public PrefixExpression(TokenType pOperator, IExpression pRight)
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

        private readonly TokenType Operator;
        private readonly IExpression Right;        
    }
}