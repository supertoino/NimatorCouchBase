using NimatorCouchBase.Entities.L.Parser.Entities.Infix.Expressions;
using NimatorCouchBase.Entities.L.Parser.Entities.Infix.Interfaces;
using NimatorCouchBase.Entities.L.Parser.Entities.Interfaces;
using NimatorCouchBase.Entities.L.Tokens;

namespace NimatorCouchBase.Entities.L.Parser.Entities.Infix
{
    public class LogicalOperatorParser : IInfixParser
    {
        public LogicalOperatorParser(int pPrecedence)
        {
            Precedence = pPrecedence;
        }

        public int Precedence { get; private set; }
        public IExpression Parse(Parser pArser, IExpression pLeft, Token pToken)
        {
            IExpression right = pArser.ParseExpression(Precedence);
            return new LogicalOperatorExpression(pLeft, pToken.Type, right);
        }        
    }
}