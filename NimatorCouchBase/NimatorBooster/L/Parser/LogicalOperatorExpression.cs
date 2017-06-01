using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using NimatorCouchBase.NimatorBooster.L.Tokens;

namespace NimatorCouchBase.NimatorBooster.L.Parser
{
    public class LogicalOperatorExpression : IExpression
    {
        private readonly IExpression LeftExpression;
        private readonly IExpression RigthExpression;
        private readonly LTokenType Operator;

        public LogicalOperatorExpression(IExpression pLeftExpression, LTokenType pOperator, IExpression pRigthExpression)
        {
            Operator = pOperator;
            RigthExpression = pRigthExpression;
            LeftExpression = pLeftExpression;
        }

        public object Value
        {
            get
            {
                dynamic leftValue = GetValueFromExpression(LeftExpression);
                dynamic rightValue = GetValueFromExpression(RigthExpression);
                return GetOperationResult(leftValue, rightValue, Operator);
            }
        }

        private object GetOperationResult(dynamic pLeftValue, dynamic pRightValue, LTokenType pLTokenType)
        {
            switch (pLTokenType)
            {
                case LTokenType.Equal:
                    return pLeftValue == pRightValue;
                case LTokenType.Bigger:
                    return pLeftValue > pRightValue;
                case LTokenType.Smaller:
                    return pLeftValue < pRightValue;
                case LTokenType.Different:
                    return pLeftValue != pRightValue;
                case LTokenType.BiggerEqual:
                    return pLeftValue >= pRightValue;
                case LTokenType.SmallerEqual:
                    return pLeftValue <= pRightValue;
                default:
                    throw new UnableToParseLTokenTypeException($"Incorrent L Validation Sintax. Unable To Validate Logical Operator {pLTokenType}");
            }
        }

        private dynamic GetValueFromExpression(IExpression pExpression)
        {
            return ExpressionUtils.GetValueFromExpression(pExpression);
        }

        private static bool ExpressionValueIsDouble(object pValue)
        {
            double isDouble;
            return double.TryParse(Convert.ToString(pValue, CultureInfo.InvariantCulture), out isDouble);
        }

        public void Print(StringBuilder pBuilder)
        {
            LeftExpression.Print(pBuilder);
            pBuilder.Append(" ").Append(Operator.GetFunctionSyntax()).Append(" ");
            RigthExpression.Print(pBuilder);
        }
    }
}