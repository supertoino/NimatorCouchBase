using NimatorCouchBase.NimatorBooster.L.Tokens;

namespace NimatorCouchBase.NimatorBooster.L.Parser
{
    public class ArithmeticDivisionExpression : ArithmeticOperatorExpression
    {
        public ArithmeticDivisionExpression(IExpression pLeftExpression, IExpression pRigthExpression) : base(pLeftExpression, LTokenType.Divide, pRigthExpression)
        {
        }

        public override object Value
        {
            get
            {
                dynamic leftValue = GetValueFromExpression(LeftExpression);
                dynamic rightValue = GetValueFromExpression(RigthExpression);
                return leftValue / rightValue;
            }
        }
    }
}