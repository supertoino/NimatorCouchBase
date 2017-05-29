using NimatorCouchBase.NimatorBooster.L.Tokens;

namespace NimatorCouchBase.NimatorBooster.L.Parser
{
    public class ArithmeticAdditionExpression : ArithmeticOperatorExpression
    {        
        public ArithmeticAdditionExpression(IExpression pLeftExpression, IExpression pRigthExpression) : base(pLeftExpression, LTokenType.Plus, pRigthExpression)
        {
        }

        public override object Value
        {
            get
            {
                dynamic leftValue = GetValueFromExpression(LeftExpression);
                dynamic rightValue = GetValueFromExpression(RigthExpression);
                return leftValue + rightValue;
            }
        }      
    }
}