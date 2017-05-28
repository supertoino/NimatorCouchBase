using System;
using System.Text;
using NimatorCouchBase.Entities.L.Memory;
using NimatorCouchBase.Entities.L.Parser.Expressions.Interfaces;
using NimatorCouchBase.Entities.L.Tokens;
using NimatorCouchBase.Utils;

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
                dynamic leftValue = GetValueFromExpression(LeftExpression);
                dynamic rightValue = GetValueFromExpression(RigthExpression);
                return GetOperationResult(leftValue, rightValue, Operator);
            }
        }

        private object GetOperationResult(dynamic pLeftValue, dynamic pRightValue, TokenType pTokenType)
        {
            switch (pTokenType)
            {
                case TokenType.Equal:
                    return pLeftValue == pRightValue;
                case TokenType.Bigger:
                    return pLeftValue > pRightValue;
                case TokenType.Smaller:
                    return pLeftValue < pRightValue;
                case TokenType.Different:
                    return pLeftValue != pRightValue;
                case TokenType.BiggerEqual:
                    return pLeftValue >= pRightValue;
                default:
                    return pLeftValue <= pRightValue;
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
                else if (LeftExpression is DoubleExpression)
                {
                    expressionValue = Convert.ToDouble(pExpression.Value);
                }
                else
                {
                    //Assume it's variable
                    var variable = (MemorySlot) expressionValue;
                    if (variable.IsEmpty())
                    {
                        throw new Exception($"Memory Slot {variable.Key} is empty");
                    }
                    expressionValue = Convert.ChangeType(variable.Value, variable.ValueType);
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Error parsing value {expressionValue} - {e.GetAllExceptionMessages()}");
            }
            return expressionValue;
        }

        public void Print(StringBuilder pBuilder)
        {
            LeftExpression.Print(pBuilder);
            pBuilder.Append(" ").Append(Operator.GetPunctuator()).Append(" ");
            RigthExpression.Print(pBuilder);
        }
    }
}