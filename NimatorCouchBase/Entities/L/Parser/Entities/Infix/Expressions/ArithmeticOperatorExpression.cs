using System;
using System.Text;
using NimatorCouchBase.Entities.L.Memory;
using NimatorCouchBase.Entities.L.Parser.Entities.Interfaces;
using NimatorCouchBase.Entities.L.Parser.Entities.Prefix.Expressions;
using NimatorCouchBase.Entities.L.Parser.Entities.Prefix.Interfaces;
using NimatorCouchBase.Entities.L.Tokens;
using NimatorCouchBase.Utils;

namespace NimatorCouchBase.Entities.L.Parser.Entities.Infix.Expressions
{
    public abstract class ArithmeticOperatorExpression : IExpression
    {
        protected readonly IExpression LeftExpression;
        protected readonly IExpression RigthExpression;
        protected readonly TokenType Operator;

        protected ArithmeticOperatorExpression(IExpression pLeftExpression, TokenType pOperator, IExpression pRigthExpression)
        {
            Operator = pOperator;
            RigthExpression = pRigthExpression;
            LeftExpression = pLeftExpression;
        }
       
        protected object GetOperationResult(dynamic pLeftValue, dynamic pRightValue, TokenType pTokenType)
        {
            switch (pTokenType)
            {
                case TokenType.Asterisk:
                    return pLeftValue * pRightValue;
                case TokenType.Plus:
                    return pLeftValue + pRightValue;
                case TokenType.Minus:
                    return pLeftValue - pRightValue;
                case TokenType.Divide:
                    return pLeftValue / pRightValue;                
                default:
                    return pLeftValue + pRightValue;
            }
        }

        protected dynamic GetValueFromExpression(IExpression pExpression)
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
                    expressionValue = Convert.ToDouble(pExpression.Value, System.Globalization.CultureInfo.InvariantCulture);
                }
                else if (pExpression is ArithmeticOperatorExpression)
                {                    
                    expressionValue = Convert.ToDouble(pExpression.Value, System.Globalization.CultureInfo.InvariantCulture);
                }
                else
                {
                    //Assume it's variable
                    var variable = (MemorySlot)expressionValue;
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

        public abstract object Value { get; }

        public void Print(StringBuilder pBuilder)
        {
            LeftExpression.Print(pBuilder);
            pBuilder.Append(" ").Append(Operator.GetFunctionSyntax()).Append(" ");
            RigthExpression.Print(pBuilder);
        }

        public string OperatorValue { get; set; }
    }
}