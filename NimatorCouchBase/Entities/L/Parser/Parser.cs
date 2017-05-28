// 
// NimatorCouchBase - NimatorCouchBase - Parser.cs 
// CREATOR: antonio.silva - António Silva
// AT: 2017/05/28/00:29
// LAST HEADER UPDATE: 2017 /05/28/20:00
// 

#region Imports

using System.Collections.Generic;
using System.Linq;
using NimatorCouchBase.Entities.L.Memory.Interfaces;
using NimatorCouchBase.Entities.L.Parser.Entities.Infix.Interfaces;
using NimatorCouchBase.Entities.L.Parser.Entities.Interfaces;
using NimatorCouchBase.Entities.L.Parser.Entities.Prefix;
using NimatorCouchBase.Entities.L.Parser.Entities.Prefix.Interfaces;
using NimatorCouchBase.Entities.L.Tokens;

#endregion

namespace NimatorCouchBase.Entities.L.Parser
{
    public class Parser
    {
        private readonly List<Token> Read;
        private readonly IEnumerator<Token> Tokens;

        private readonly IMemory Memory;
        
        private readonly Dictionary<TokenType, IPrefixParser> PrefixParser = new Dictionary<TokenType, IPrefixParser>();
        private readonly Dictionary<TokenType, IInfixParser> InfixParsers = new Dictionary<TokenType, IInfixParser>();

        private Parser()
        {
            Read = new List<Token>();
            Memory = null;
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

        public IExpression ParseExpression(int pPrecedence = 0)
        {
            return TopDownOperatorPredenceAlgorithm(pPrecedence);
        }

        private IExpression TopDownOperatorPredenceAlgorithm(int pPrecedence = 0)
        {
            Token token = Consume();
            //Longs, Doubles and Variables
            var prefixParser = PrefixParser[token.Type];

            if (prefixParser is VariableParser && MemoryAvaiable())
            {
                var variableParser = (VariableParser) prefixParser;
                variableParser.SetMemory(Memory);
            }

            IExpression leftExpression = prefixParser.Parse(this, token); //Get Value for Prefix Value

            while (CurrentPrecedenceIsLowerThenNextToken(pPrecedence))
            {
                token = Consume(); //Advance for next token. The Next token is Infix
                var infixParser = InfixParsers[token.Type];
                leftExpression = infixParser.Parse(this, leftExpression, token);
                //Continue doing this until the precedence for the LookAhead (i.e, is higher then current token)
            }
            return leftExpression;
        }

        private bool CurrentPrecedenceIsLowerThenNextToken(int pPrecedence)
        {
            return pPrecedence < GetPrecedenceForNextToken();
        }

        private bool MemoryAvaiable()
        {
            return Memory != null;
        }

        private int GetPrecedenceForNextToken()
        {
            Token lookAhead = LookAhead(0);
            if (lookAhead == null)
            {
                return 0;
            }
            var parser = InfixParsers[lookAhead.Type];
            if (parser != null)
            {
                return parser.GetPrecedence();
            }
            return 0;
        }

        public void AddParser(TokenType pToken, IPrefixParser pArselet)
        {
            PrefixParser.Add(pToken, pArselet);
        }

        public void AddParser(TokenType pToken, IInfixParser pArselet)
        {
            InfixParsers.Add(pToken, pArselet);
        }
    }
}