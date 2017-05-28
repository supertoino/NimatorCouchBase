using NimatorCouchBase.Entities.L.Tokens;

namespace NimatorCouchBase.Entities.L.Parser
{
    public class ArithmenticSubtractionOperatorParser : IInfixParser
    {
        public ArithmenticSubtractionOperatorParser(int pPrecedence)
        {
            Precedence = pPrecedence;
        }

        public IExpression Parse(Parser pArser, IExpression pLeft, Token pToken)
        {
            IExpression right = pArser.ParseExpression(Precedence);
            return new ArithmeticSubtractionExpression(pLeft, right);
        }

        public int Precedence { get; }
    }
}