using System.Collections.Generic;
using System.Linq;

namespace NimatorCouchBase.Entities.L
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
                Tokens.MoveNext();
                Read.Add(Tokens.Current);
            }

            // Get the queued token.
            return Read[pDistance];
        }

    }
}