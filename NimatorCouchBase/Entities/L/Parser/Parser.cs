using System.Collections.Generic;
using System.Linq;
using NimatorCouchBase.Entities.L.Memory.Interfaces;
using NimatorCouchBase.Entities.L.Parser.Expressions;
using NimatorCouchBase.Entities.L.Parser.Expressions.Interfaces;
using NimatorCouchBase.Entities.L.Parser.Interfaces;
using NimatorCouchBase.Entities.L.Parser.Specific;
using NimatorCouchBase.Entities.L.Tokens;

namespace NimatorCouchBase.Entities.L.Parser
{
    public class Parser
    {
        private readonly IEnumerator<Token> Tokens;
        private readonly List<Token> Read;
        private readonly Dictionary<TokenType, IInfixParser> InfixParsers = new Dictionary<TokenType, IInfixParser>();
        private readonly Dictionary<TokenType, IPrefixParser> PrefixParser = new Dictionary<TokenType, IPrefixParser>();

        private readonly IMemory Memory;

        private Parser()
        {
            Read = new List<Token>();
            Memory = new Memory.Memory();
        }
        public Parser(IEnumerator<Token> pTokens) : this()
        {
            Tokens = pTokens;            
        }

        public Parser(IEnumerator<Token> pTokens, IMemory pMemory) : this()
        {
            Tokens = pTokens;            
            Memory = pMemory;
        }

        public Token Consume()
        {
            LookAhead(0);

            var nextToken = Read[0];
            Read.RemoveAt(0);
            return nextToken;
        }

        private Token LookAhead(int pDistance)
        {
            while (pDistance >= Read.Count())
            {
                Read.Add(Tokens.MoveNext() ? Tokens.Current : null);
            }

            // Get the queued token.
            return Read[pDistance];
        }

        public IExpression ParseExpression()
        {
            Token token = Consume();
            IPrefixParser prefix = PrefixParser[token.Type];
            if (prefix is VariableParser)
            {
                //Add Memory Access to VariableParser
                var variableParser = (VariableParser) prefix;
                variableParser.SetMemory(Memory);
            }

            //if (prefix == null) throw new ParseException("Could not parse \"" + token.getText() + "\".");

            IExpression left = prefix.Parse(this, token);

            token = LookAhead(0);

            //If no token is found then return the left value alone.
            if (token == null) return left;

            IInfixParser infix = InfixParsers[token.Type];

            // No infix expression at this point, so we're done.
            if (infix == null) return left;

            Consume();
            return infix.Parse(this, left, token);
        }


        public void Register(TokenType pToken, IPrefixParser pArselet)
        {
            PrefixParser.Add(pToken, pArselet);
        }

        public void Register(TokenType pToken, IInfixParser pArselet)
        {
            InfixParsers.Add(pToken, pArselet);
        }
    }
}