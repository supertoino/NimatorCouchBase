using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using NimatorCouchBase.Entities.L.Expressions;

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

    public class ScalarParselt : IPrefixParselet
    {
        public IExpression Parse(Parser pArser, Token pToken)
        {
            return new ScalarExpression(pToken.Value);
        }
    }

    public interface IInfixParselet
    {
        IExpression Parse(Parser pArser, IExpression pLeft, Token pToken);
    }

    public interface IPrefixParselet
    {
        IExpression Parse(Parser pArser, Token pToken);
    }

    public class PrefixOperatorParselet : IPrefixParselet
    {
        public IExpression Parse(Parser pArser, Token pToken)
        {
            IExpression operand = pArser.ParseExpression();
            return new PrefixExpression(pToken.Type, operand);
        }
    }

    public class PrefixExpression : IExpression
    {
        public PrefixExpression(TokenType pOperatorr, IExpression pRight)
        {
            MOperator = pOperatorr;
            MRight = pRight;
        }

        public void Print(StringBuilder pBuilder)
        {
            pBuilder.Append("(").Append(MOperator.GetPunctuator());
            MRight.Print(pBuilder);
            pBuilder.Append(")");
        }

        private readonly TokenType MOperator;
        private readonly IExpression MRight;        
    }

    public class BinaryOperatorParselet : IInfixParselet
    {
        public IExpression Parse(Parser pArser, IExpression pLeft, Token pToken)
        {
            IExpression right = pArser.ParseExpression();
            return new OperatorExpression(pLeft, pToken.Type, right);
        }
    }
}