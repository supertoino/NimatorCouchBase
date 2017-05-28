using System.Collections.Generic;
using NimatorCouchBase.Entities.L.Memory.Interfaces;
using NimatorCouchBase.Entities.L.Parser.Specific;
using NimatorCouchBase.Entities.L.Tokens;

namespace NimatorCouchBase.Entities.L.Parser
{
    public class LParser : Parser
    {        
        public LParser(IEnumerator<Token> pTokens) : base(pTokens)
        {
            Register(TokenType.Long, new LongParser());
            Register(TokenType.Double, new DoubleParser());
            Register(TokenType.Variable, new VariableParser());

            Register(TokenType.Bigger, new BinaryOperatorParser());    
            Register(TokenType.Equal, new BinaryOperatorParser());    
            Register(TokenType.Smaller, new BinaryOperatorParser());
            Register(TokenType.Different, new BinaryOperatorParser());
            Register(TokenType.BiggerEqual, new BinaryOperatorParser());
            Register(TokenType.SmallerEqual, new BinaryOperatorParser());
        }

        public LParser(IEnumerator<Token> pTokens, IMemory pMemory) : base(pTokens, pMemory)
        {
            Register(TokenType.Long, new LongParser());
            Register(TokenType.Double, new DoubleParser());
            Register(TokenType.Variable, new VariableParser());

            Register(TokenType.Bigger, new BinaryOperatorParser());
            Register(TokenType.Equal, new BinaryOperatorParser());
            Register(TokenType.Smaller, new BinaryOperatorParser());
            Register(TokenType.Different, new BinaryOperatorParser());
            Register(TokenType.BiggerEqual, new BinaryOperatorParser());
            Register(TokenType.SmallerEqual, new BinaryOperatorParser());
        }
    }
}