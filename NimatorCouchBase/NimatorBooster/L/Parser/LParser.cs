// 
// NimatorCouchBase - NimatorCouchBase - LParser.cs 
// CREATOR: antonio.silva - António Silva
// AT: 2017/05/28/00:29
// LAST HEADER UPDATE: 2017 /05/28/16:41
// 

#region Imports

using System;
using System.Collections.Generic;
using NimatorCouchBase.NimatorBooster.L.Parser.Storage;
using NimatorCouchBase.NimatorBooster.L.Tokens;

#endregion

namespace NimatorCouchBase.NimatorBooster.L.Parser
{
    public class LParser : BaseParser
    {
        public LParser(IEnumerator<LToken> pTokens) : base(pTokens)
        {
            AddValidParsers();
        }

        public LParser(IEnumerator<LToken> pTokens, IMemory pMemory) : base(pTokens, pMemory)
        {
            AddValidParsers();
        }

        public bool Parse()
        {
            IExpression expression = this.ParseExpression();
            return Convert.ToBoolean(expression.Value);
        }

        private void AddValidParsers()
        {
            AddPrefixParsers();
            AddInfixParsers();
        }

        private void AddInfixParsers()
        {
            const int PRECEDENCE_LOGICAL_OPERATORS = 5;
            const int PRECEDENCE_ARITHMETIC_SUM = 10;
            const int PRECEDENCE_ARITHMETIC_MUL = 20;

            AddInfixExpressionParser(LTokenType.Plus, new ArithmenticAdditionOperatorParser(PRECEDENCE_ARITHMETIC_SUM));
            AddInfixExpressionParser(LTokenType.Minus, new ArithmenticSubtractionOperatorParser(PRECEDENCE_ARITHMETIC_SUM));
            
            AddInfixExpressionParser(LTokenType.Asterisk, new ArithmenticMultiplicationOperatorParser(PRECEDENCE_ARITHMETIC_MUL));
            AddInfixExpressionParser(LTokenType.Divide, new ArithmenticDivisionOperatorParser(PRECEDENCE_ARITHMETIC_MUL));
            
            AddInfixExpressionParser(LTokenType.Bigger, new LogicalOperatorParser(PRECEDENCE_LOGICAL_OPERATORS));
            AddInfixExpressionParser(LTokenType.Equal, new LogicalOperatorParser(PRECEDENCE_LOGICAL_OPERATORS));
            AddInfixExpressionParser(LTokenType.Smaller, new LogicalOperatorParser(PRECEDENCE_LOGICAL_OPERATORS));
            AddInfixExpressionParser(LTokenType.Different, new LogicalOperatorParser(PRECEDENCE_LOGICAL_OPERATORS));
            AddInfixExpressionParser(LTokenType.BiggerEqual, new LogicalOperatorParser(PRECEDENCE_LOGICAL_OPERATORS));
            AddInfixExpressionParser(LTokenType.SmallerEqual, new LogicalOperatorParser(PRECEDENCE_LOGICAL_OPERATORS));
        }

        private void AddPrefixParsers()
        {
            AddPrefixExpressionParser(LTokenType.Long, new LongParser());
            AddPrefixExpressionParser(LTokenType.Double, new DoubleParser());
            AddPrefixExpressionParser(LTokenType.Variable, new VariableParser());
        }        
    }
}