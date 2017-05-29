using NimatorCouchBase.NimatorBooster.L.Tokens;

namespace NimatorCouchBase.NimatorBooster.L.Parser
{
    public class LogicalOperatorParser : IInfixParser
    {
        public LogicalOperatorParser(int pPrecedence)
        {
            Precedence = pPrecedence;
        }

        public int Precedence { get; private set; }
        public IExpression Parse(BaseParser pArser, IExpression pLeft, LToken pLToken)
        {
            IExpression right = pArser.ParseExpression(Precedence);
            return new LogicalOperatorExpression(pLeft, pLToken.Type, right);
        }        
    }
}