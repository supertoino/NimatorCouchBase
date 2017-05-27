using System.Text;

namespace NimatorCouchBase.Entities.L
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

        public void Print(StringBuilder pBuilder)
        {
            pBuilder.Append("(");
            LeftExpression.Print(pBuilder);
            pBuilder.Append(" ").Append(Operator.GetPunctuator()).Append(" ");
            RigthExpression.Print(pBuilder);
            pBuilder.Append(")");
        }
    }
}