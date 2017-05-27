using System.Collections.Generic;
using System.Linq;
using NimatorCouchBase.Entities.L.Parser.Expressions;
using NimatorCouchBase.Entities.L.Tokens;

namespace NimatorCouchBase.Entities.L.Parser
{
    public class Parser
    {
        private readonly IEnumerator<Token> Tokens;
        private readonly List<Token> Read;

        public Parser(IEnumerator<Token> pTokens)
        {
            Tokens = pTokens;
            Read = new List<Token>();
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
            IPrefixParselet prefix = MiPrefixParselet[token.Type];

            //if (prefix == null) throw new ParseException("Could not parse \"" + token.getText() + "\".");

            IExpression left = prefix.Parse(this, token);

            token = LookAhead(0);
            if (token == null) return left;
            IInfixParselet infix = MInfixParselets[token.Type];

            // No infix expression at this point, so we're done.
            if (infix == null) return left;

            Consume();
            return infix.Parse(this, left, token);
        }


        public void Register(TokenType pToken, IPrefixParselet pArselet)
        {
            MiPrefixParselet.Add(pToken, pArselet);
        }

        public void Register(TokenType pToken, IInfixParselet pArselet)
        {
            MInfixParselets.Add(pToken, pArselet);
        }

        private readonly Dictionary<TokenType, IInfixParselet> MInfixParselets = new Dictionary<TokenType, IInfixParselet>();
        private readonly Dictionary<TokenType, IPrefixParselet> MiPrefixParselet = new Dictionary<TokenType, IPrefixParselet>();

    }
}