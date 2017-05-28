// 
// NimatorCouchBase - NimatorCouchBase - LParser.cs 
// CREATOR: antonio.silva - António Silva
// AT: 2017/05/28/00:29
// LAST HEADER UPDATE: 2017 /05/28/16:41
// 

#region Imports

using System.Collections.Generic;
using NimatorCouchBase.Entities.L.Memory.Interfaces;
using NimatorCouchBase.Entities.L.Parser.Entities.Infix;
using NimatorCouchBase.Entities.L.Parser.Entities.Prefix;
using NimatorCouchBase.Entities.L.Tokens;

#endregion

namespace NimatorCouchBase.Entities.L.Parser
{
    public class LParser : Parser
    {
        public LParser(IEnumerator<Token> pTokens) : base(pTokens)
        {
            AddValidParsers();
        }

        public LParser(IEnumerator<Token> pTokens, IMemory pMemory) : base(pTokens, pMemory)
        {
            AddValidParsers();
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

            AddParser(TokenType.Plus, new ArithmenticAdditionOperatorParser(PRECEDENCE_ARITHMETIC_SUM));
            AddParser(TokenType.Minus, new ArithmenticSubtractionOperatorParser(PRECEDENCE_ARITHMETIC_SUM));
            
            AddParser(TokenType.Asterisk, new ArithmenticMultiplicationOperatorParser(PRECEDENCE_ARITHMETIC_MUL));
            AddParser(TokenType.Divide, new ArithmenticDivisionOperatorParser(PRECEDENCE_ARITHMETIC_MUL));
            
            AddParser(TokenType.Bigger, new LogicalOperatorParser(PRECEDENCE_LOGICAL_OPERATORS));
            AddParser(TokenType.Equal, new LogicalOperatorParser(PRECEDENCE_LOGICAL_OPERATORS));
            AddParser(TokenType.Smaller, new LogicalOperatorParser(PRECEDENCE_LOGICAL_OPERATORS));
            AddParser(TokenType.Different, new LogicalOperatorParser(PRECEDENCE_LOGICAL_OPERATORS));
            AddParser(TokenType.BiggerEqual, new LogicalOperatorParser(PRECEDENCE_LOGICAL_OPERATORS));
            AddParser(TokenType.SmallerEqual, new LogicalOperatorParser(PRECEDENCE_LOGICAL_OPERATORS));
        }

        private void AddPrefixParsers()
        {
            AddParser(TokenType.Long, new LongParser());
            AddParser(TokenType.Double, new DoubleParser());
            AddParser(TokenType.Variable, new VariableParser());
        }
    }
}