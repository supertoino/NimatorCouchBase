using System.Text;
using NimatorCouchBase.Entities.L.Parser.Expressions;
using NimatorCouchBase.Entities.L.Tokens;

namespace NimatorCouchBase.Entities.L.Parser
{
    public class PrefixExpression : IExpression
    {
        public PrefixExpression(TokenType pOperatorr, IExpression pRight)
        {
            MOperator = pOperatorr;
            MRight = pRight;
        }

        public object Value { get; }

        public void Print(StringBuilder pBuilder)
        {
            pBuilder.Append("(").Append(MOperator.GetPunctuator());
            MRight.Print(pBuilder);
            pBuilder.Append(")");
        }

        private readonly TokenType MOperator;
        private readonly IExpression MRight;        
    }
}