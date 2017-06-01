using NimatorCouchBase.NimatorBooster.L.Tokens;

namespace NimatorCouchBase.NimatorBooster.L.Parser
{
    public class BaseArithmeticSubtractionExpression : BaseArithmeticOperatorExpression
    {
        public BaseArithmeticSubtractionExpression(IExpression pLeftExpression, IExpression pRigthExpression) : base(pLeftExpression, LTokenType.Minus, pRigthExpression)
        {
        }

        public override object Value
        {
            get
            {
                dynamic leftValue = GetValueFromExpression(LeftExpression);
                dynamic rightValue = GetValueFromExpression(RigthExpression);
                return leftValue - rightValue;
            }
        }
    }
}