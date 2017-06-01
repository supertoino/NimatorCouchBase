using NimatorCouchBase.NimatorBooster.L.Tokens;

namespace NimatorCouchBase.NimatorBooster.L.Parser
{
    public class ArithmenticAdditionOperatorParser : IInfixParser
    {
        public ArithmenticAdditionOperatorParser(int pPrecedence)
        {
            Precedence = pPrecedence;
        }

        public IExpression Parse(BaseParser pArser, IExpression pLeft, LToken pLToken)
        {
            IExpression right = pArser.ParseExpression(Precedence);
            return new BaseArithmeticAdditionExpression(pLeft, right);
        }

        public int Precedence { get; }       
    }
}