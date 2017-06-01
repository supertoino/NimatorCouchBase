using System;
using NimatorCouchBase.NimatorBooster.L.Tokens;

namespace NimatorCouchBase.NimatorBooster.L.Parser
{
    public class BaseArithmeticDivisionExpression : BaseArithmeticOperatorExpression
    {
        public BaseArithmeticDivisionExpression(IExpression pLeftExpression, IExpression pRigthExpression) : base(pLeftExpression, LTokenType.Divide, pRigthExpression)
        {
        }

        public override object Value
        {
            get
            {
                dynamic leftValue = GetValueFromExpression(LeftExpression);
                dynamic rightValue = GetValueFromExpression(RigthExpression);
                if (rightValue == 0)
                {
                    throw new DivideByZeroException();
                }
                return (double) leftValue / (double) rightValue;
            }
        }
    }
}