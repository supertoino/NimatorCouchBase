using NimatorCouchBase.NimatorBooster.L.Tokens;

namespace NimatorCouchBase.NimatorBooster.L.Parser
{
    public class ArithmenticSubtractionOperatorParser : IInfixParser
    {
        public ArithmenticSubtractionOperatorParser(int pPrecedence)
        {
            Precedence = pPrecedence;
        }

        public IExpression Parse(BaseParser pArser, IExpression pLeft, LToken pLToken)
        {
            IExpression right = pArser.ParseExpression(Precedence);
            return new BaseArithmeticSubtractionExpression(pLeft, right);
        }

        public int Precedence { get; }
    }
}