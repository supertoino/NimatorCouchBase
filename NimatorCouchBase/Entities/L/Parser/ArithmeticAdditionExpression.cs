using NimatorCouchBase.Entities.L.Tokens;

namespace NimatorCouchBase.Entities.L.Parser
{
    public class ArithmeticAdditionExpression : ArithmeticOperatorExpression
    {        
        public ArithmeticAdditionExpression(IExpression pLeftExpression, IExpression pRigthExpression) : base(pLeftExpression, TokenType.Plus, pRigthExpression)
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