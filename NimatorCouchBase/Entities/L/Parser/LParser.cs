using System.Collections.Generic;
using NimatorCouchBase.Entities.L.Tokens;

namespace NimatorCouchBase.Entities.L.Parser
{
    public class LParser : Parser
    {
        public LParser(IEnumerator<Token> pTokens) : base(pTokens)
        {
            Register(TokenType.Scalar, new ScalarParselt() );

            Register(TokenType.Bigger, new BinaryOperatorParselet());    
            Register(TokenType.Equal, new BinaryOperatorParselet());    
            Register(TokenType.Smaller, new BinaryOperatorParselet());    
        }
    }
}