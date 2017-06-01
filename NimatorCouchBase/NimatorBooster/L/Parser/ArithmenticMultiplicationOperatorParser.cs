using NimatorCouchBase.NimatorBooster.L.Tokens;

namespace NimatorCouchBase.NimatorBooster.L.Parser
{
    public class ArithmenticMultiplicationOperatorParser : IInfixParser
    {
        public ArithmenticMultiplicationOperatorParser(int pPrecedence)
        {
            Precedence = pPrecedence;
        }

        public IExpression Parse(BaseParser pArser, IExpression pLeft, LToken pLToken)
        {
            IExpression rightExpression = pArser.ParseExpression(Precedence);
            return new BaseArithmeticMultiplicationExpression(pLeft, rightExpression);
        }

        public int Precedence { get; }

    }
}