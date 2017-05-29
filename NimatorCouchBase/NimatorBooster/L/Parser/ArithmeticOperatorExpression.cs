using System;
using System.Text;
using NimatorCouchBase.NimatorBooster.L.Parser.Storage;
using NimatorCouchBase.NimatorBooster.L.Tokens;
using NimatorCouchBase.NimatorBooster.Utils;

namespace NimatorCouchBase.NimatorBooster.L.Parser
{
    public abstract class ArithmeticOperatorExpression : IExpression
    {
        protected readonly IExpression LeftExpression;
        protected readonly IExpression RigthExpression;
        protected readonly LTokenType Operator;

        protected ArithmeticOperatorExpression(IExpression pLeftExpression, LTokenType pOperator, IExpression pRigthExpression)
        {
            Operator = pOperator;
            RigthExpression = pRigthExpression;
            LeftExpression = pLeftExpression;
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
    }
}