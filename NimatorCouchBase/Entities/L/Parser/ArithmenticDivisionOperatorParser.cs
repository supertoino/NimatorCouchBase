using NimatorCouchBase.Entities.L.Tokens;

namespace NimatorCouchBase.Entities.L.Parser
{
    public class ArithmenticDivisionOperatorParser : IInfixParser
    {
        public ArithmenticDivisionOperatorParser(int pPrecedence)
        {
            Precedence = pPrecedence;
        }

        public IExpression Parse(Parser pArser, IExpression pLeft, Token pToken)
        {
            IExpression right = pArser.ParseExpression(Precedence);
            return new ArithmeticDivisionExpression(pLeft, right);
        }

        public int Precedence { get; }
    }
}