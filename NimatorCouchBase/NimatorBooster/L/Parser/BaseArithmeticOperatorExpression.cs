// 
// NimatorCouchBase - NimatorCouchBase - BaseArithmeticOperatorExpression.cs 
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
    public abstract class BaseArithmeticOperatorExpression : IExpression
    {
        protected readonly IExpression LeftExpression;
        protected readonly LTokenType Operator;
        protected readonly IExpression RigthExpression;

        protected BaseArithmeticOperatorExpression(IExpression pLeftExpression, LTokenType pOperator,
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
            return ExpressionUtils.GetValueFromExpression(pExpression);
        }       
    }
}