using NimatorCouchBase.Entities.L.Parser.Entities.Interfaces;
using NimatorCouchBase.Entities.L.Tokens;

namespace NimatorCouchBase.Entities.L.Parser.Entities.Infix.Expressions
{
    public class ArithmeticSubtractionExpression : ArithmeticOperatorExpression
    {
        public ArithmeticSubtractionExpression(IExpression pLeftExpression, IExpression pRigthExpression) : base(pLeftExpression, TokenType.Minus, pRigthExpression)
        {
        }

        public override object Value
        {
            get
            {
                dynamic leftValue = GetValueFromExpression(LeftExpression);
                dynamic rightValue = GetValueFromExpression(RigthExpression);
                return leftValue * rightValue;
            }
        }
    }
}