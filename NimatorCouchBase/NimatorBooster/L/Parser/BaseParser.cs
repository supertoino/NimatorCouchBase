// 
// NimatorCouchBase - NimatorCouchBase - Parser.cs 
// CREATOR: antonio.silva - António Silva
// AT: 2017/05/28/00:29
// LAST HEADER UPDATE: 2017 /05/28/20:00
// 

#region Imports

using System.Collections.Generic;
using System.Linq;
using NimatorCouchBase.NimatorBooster.L.Parser.Storage.Interfaces;
using NimatorCouchBase.NimatorBooster.L.Tokens;

#endregion

namespace NimatorCouchBase.NimatorBooster.L.Parser
{
    public abstract class BaseParser
    {
        private readonly List<LToken> Read;
        private readonly IEnumerator<LToken> Tokens;

        private readonly IMemory Memory;

        private readonly Dictionary<LTokenType, IPrefixParser> PrefixParser;
        private readonly Dictionary<LTokenType, IInfixParser> InfixParsers;

        private BaseParser()
        {
            Read = new List<LToken>();
            Memory = null;

            PrefixParser = new Dictionary<LTokenType, IPrefixParser>();
            InfixParsers = new Dictionary<LTokenType, IInfixParser>();
        }

        protected BaseParser(IEnumerator<LToken> pTokens) : this()
        {
            Tokens = pTokens;
        }

        protected BaseParser(IEnumerator<LToken> pTokens, IMemory pMemory) : this()
        {
            Tokens = pTokens;
            Memory = pMemory;
        }

        public LToken Consume()
        {
            LookAhead(0);
            var nextToken = Read[0];
            Read.RemoveAt(0);
            return nextToken;
        }

        private LToken LookAhead(int pDistance)
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
            return TopDownOperatorPrecedenceAlgorithm(pPrecedence);
        }

        private IExpression TopDownOperatorPrecedenceAlgorithm(int pPrecedence = 0)
        {
            LToken lToken = Consume();
            //Longs, Doubles and Variables
            var prefixParser = GetPrefixParserByTokenType(lToken.Type);

            if (prefixParser is VariableParser && MemoryAvaiable())
            {
                AddMemoryToExpressionParser(prefixParser);
            }

            IExpression leftExpression = prefixParser.Parse(this, lToken); //Get Value for Prefix Value

            while (CurrentPrecedenceIsLowerThenNextToken(pPrecedence))
            {
                lToken = Consume(); //Advance for next LToken. The Next LToken is Infix
                var infixParser = GetInfixParserByTokenType(lToken.Type);
                leftExpression = infixParser.Parse(this, leftExpression, lToken);
                //Continue doing this until the precedence is lower for the LookAhead
            }
            return leftExpression;
        }

        private IInfixParser GetInfixParserByTokenType(LTokenType pLTokenType)
        {
            if (!InfixParsers.ContainsKey(pLTokenType))
            {
                return null;
            }
            return InfixParsers[pLTokenType];
        }

        private IPrefixParser GetPrefixParserByTokenType(LTokenType pLTokenType)
        {
            if (!PrefixParser.ContainsKey(pLTokenType))
            {
                return null;
            }
            return PrefixParser[pLTokenType];
        }

        private void AddMemoryToExpressionParser(IPrefixParser pPrefixParser)
        {
            var variableParser = (VariableParser) pPrefixParser;
            variableParser?.SetMemory(Memory);
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
            LToken lookAhead = LookAhead(0);
            if (lookAhead == null)
            {
                return 0;
            }
            var parser = InfixParsers[lookAhead.Type];
            if (parser != null)
            {
                return parser.Precedence;
            }
            return 0;
        }

        public void AddPrefixExpressionParser(LTokenType pLToken, IPrefixParser pArselet)
        {
            PrefixParser.Add(pLToken, pArselet);
        }

        public void AddInfixExpressionParser(LTokenType pLToken, IInfixParser pArselet)
        {
            InfixParsers.Add(pLToken, pArselet);
        }
    }
}