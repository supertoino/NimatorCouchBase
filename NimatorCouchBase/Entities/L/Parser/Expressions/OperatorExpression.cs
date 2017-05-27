using System;
using System.Text;
using NimatorCouchBase.Entities.L.Tokens;

namespace NimatorCouchBase.Entities.L.Parser.Expressions
{
    public class OperatorExpression : IExpression
    {
        private readonly IExpression LeftExpression;
        private readonly IExpression RigthExpression;
        private readonly TokenType Operator;

        public OperatorExpression(IExpression pLeftExpression, TokenType pOperator, IExpression pRigthExpression)
        {
            Operator = pOperator;
            RigthExpression = pRigthExpression;
            LeftExpression = pLeftExpression;
        }

        public object Value
        {
            get
            {
                if (Operator == TokenType.Equal)
                {
                    return Convert.ToInt64(LeftExpression.Value) == Convert.ToInt64(RigthExpression.Value);
                }
                else
                {
                    return Convert.ToInt64(LeftExpression.Value) > Convert.ToInt64(RigthExpression.Value);
                }                
            }
        }

        public void Print(StringBuilder pBuilder)
        {
            LeftExpression.Print(pBuilder);
            pBuilder.Append(" ").Append(Operator.GetPunctuator()).Append(" ");
            RigthExpression.Print(pBuilder);
        }
    }
}