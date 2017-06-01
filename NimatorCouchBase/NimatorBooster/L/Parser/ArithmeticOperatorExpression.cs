// 
// NimatorCouchBase - NimatorCouchBase - ArithmeticOperatorExpression.cs 
// CREATOR: antonio.silva - António Silva
// AT: 2017/05/30/14:55
// LAST HEADER UPDATE: 2017 /05/30/21:26
// 

#region Imports

using System;
using System.Globalization;
using System.Text;
using NimatorCouchBase.NimatorBooster.L.Parser.Storage;
using NimatorCouchBase.NimatorBooster.L.Tokens;

#endregion

namespace NimatorCouchBase.NimatorBooster.L.Parser
{
    public abstract class ArithmeticOperatorExpression : IExpression
    {
        protected readonly IExpression LeftExpression;
        protected readonly LTokenType Operator;
        protected readonly IExpression RigthExpression;

        protected ArithmeticOperatorExpression(IExpression pLeftExpression, LTokenType pOperator,
            IExpression pRigthExpression)
        {
            Operator = pOperator;
            RigthExpression = pRigthExpression;
            LeftExpression = pLeftExpression;
        }

        public abstract object Value { get; }

        public void Print(StringBuilder pBuilder)
        {
            LeftExpression.Print(pBuilder);
            pBuilder.Append(" ").Append(Operator.GetFunctionSyntax()).Append(" ");
            RigthExpression.Print(pBuilder);
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
                    var variable = (MemorySlot)expressionValue;
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
            return double.TryParse(Convert.ToString(pValue), out isDouble);
        }
    }
}