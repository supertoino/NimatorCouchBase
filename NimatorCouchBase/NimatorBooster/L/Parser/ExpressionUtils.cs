using System;
using System.Globalization;
using NimatorCouchBase.NimatorBooster.L.Parser.Storage;

namespace NimatorCouchBase.NimatorBooster.L.Parser
{
    public static class ExpressionUtils
    {
        public static dynamic GetValueFromExpression(IExpression pExpression)
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
                else if (pExpression is BaseArithmeticOperatorExpression)
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
            return double.TryParse(Convert.ToString(pValue, CultureInfo.InvariantCulture), out isDouble);
        }
    }
}