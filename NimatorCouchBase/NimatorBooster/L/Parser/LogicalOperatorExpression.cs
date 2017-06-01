using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using NimatorCouchBase.NimatorBooster.L.Parser.Storage;
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
            dynamic expressionValue = pExpression.Value;
            try
            {
                if (pExpression is LongExpression)
                {
                    expressionValue = Convert.ToInt64(pExpression.Value);
                }
                else if (pExpression is DoubleExpression)
                {
                    expressionValue = Convert.ToDouble(pExpression.Value, CultureInfo.InvariantCulture);
                }
                else if (pExpression is ArithmeticOperatorExpression)
                {
                    object value = pExpression.Value;
                    expressionValue = ExpressionValueIsDouble(value)
                        ? Convert.ToDouble(value, CultureInfo.InvariantCulture)
                        : Convert.ToInt64(value, CultureInfo.InvariantCulture);
                }
                else if (pExpression is VariableExpression)
                {
                    var variable = (MemorySlot) expressionValue;
                    if (variable.IsEmpty())
                    {
                        throw new AccessingEmptyMemoryException($"Memory Slot {variable.Key} is empty");
                    }                    
                    expressionValue = Convert.ChangeType(variable.Value, variable.ValueType);
                }
                else
                {
                    throw new UnableToValidateExpressionException("");
                }
            }            
            catch (Exception e)
            {
                throw new UnableToValidateExpressionException($"Incorrent L Validation Sintax. Unable To Validate Value {pExpression.Value}: {e.Message}");
            }
            return expressionValue;
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