using NimatorCouchBase.NimatorBooster.L.Tokens;

namespace NimatorCouchBase.NimatorBooster.L.Parser
{
    public class ArithmenticDivisionOperatorParser : IInfixParser
    {
        public ArithmenticDivisionOperatorParser(int pPrecedence)
        {
            Precedence = pPrecedence;
        }

        public IExpression Parse(BaseParser pArser, IExpression pLeft, LToken pLToken)
        {
            IExpression right = pArser.ParseExpression(Precedence);
            return new ArithmeticDivisionExpression(pLeft, right);
        }

        public int Precedence { get; }
    }
}